using Godot;
using System;

public class NetworkSetup : Node
{
	public override void _Ready()
	{
		Network.GetInstance().HostGame();
	}
}
