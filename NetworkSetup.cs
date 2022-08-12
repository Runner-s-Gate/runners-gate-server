using Godot;
using System;

public class NetworkSetup : Node
{
	private string ipAddr = "127.0.0.1";

	public override void _Ready()
	{
		//await ToSignal(GetTree(), "process_frame");
		//Network.GetInstance().HostGame();
	}
}
