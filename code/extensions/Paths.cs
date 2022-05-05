using Sandbox;
using System.Collections.Generic;

public partial class Path : Entity
{

	public MovementPathEntity PathEntity { get; internal set; }
	public float Length { get; internal set; }
	public Dictionary<BasePathNode, float> NodeLength { get; internal set; } = new Dictionary<BasePathNode, float>();
	public Dictionary<BasePathNode, float> NodeFraction { get; internal set; } = new Dictionary<BasePathNode, float>();

	public Path() { } 

	public Path( MovementPathEntity path )
	{

		PathEntity = path;
		Length = GetPathLength();

		float totalFraction = 0f;

		for ( int i = 0; i < PathEntity.PathNodes.Count - 1; i++ )
		{

			float distance = GetNodeLength( i );
			NodeLength[PathEntity.PathNodes[i]] = distance;

			float fraction = distance / Length;
			totalFraction += fraction;
			NodeFraction[PathEntity.PathNodes[i]] = totalFraction;

		}

	}

	public float GetNodeLength( int node )
	{

		float distance = PathEntity.GetCurveLength( PathEntity.PathNodes[node], PathEntity.PathNodes[node + 1], 5 );

		return distance;

	}

	public float GetPathLength()
	{

		float totalDistance = 0f;

		for ( int i = 0; i < PathEntity.PathNodes.Count - 1; i++ )
		{

			float distance = GetNodeLength( i );
			totalDistance += distance;

		}

		return totalDistance;

	}
	
	public Vector3 GetPathPosition( float progress )
	{

		Vector3 position = PathEntity.Position;

		var nodes = PathEntity.PathNodes;

		float lastFraction = 0f;
		float currentFraction = 0f;
		int currentNode = 0;

		for ( int i = 0; i < nodes.Count - 1; i++ )
		{

			currentFraction = NodeFraction[nodes[i]];

			if ( progress < currentFraction && progress > lastFraction )
			{

				currentNode = i;
				break;

			}

			lastFraction = currentFraction;

		}

		float nodeFraction = ( progress - lastFraction ) / ( currentFraction - lastFraction );
		position = PathEntity.GetPointBetweenNodes( nodes[currentNode], nodes[currentNode + 1], nodeFraction );

		return position;


	}
	

}
