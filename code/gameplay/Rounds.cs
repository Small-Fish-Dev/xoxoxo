
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class xoxoxo : Sandbox.Game
{

	[Net] public float RoundTime { get; private set; } = 0f;


	[Event.Tick]
	public void SetTime()
	{

		RoundTime += Time.Delta;

	}

}
