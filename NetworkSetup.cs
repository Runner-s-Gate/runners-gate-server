using Godot;
using System;

public class NetworkSetup : Control
{
	private string ipAddr = "";

	private void _on_IpAddress_text_changed(String new_text)
	{
		ipAddr = new_text;
	}

	private void _on_HostButton_pressed()
	{
		Network.GetInstance().CreateServer();
		Hide();
	}

	private void _on_JoinButton_pressed()
	{
		Network.GetInstance().JoinServer(ipAddr);
		Hide();
	}
}
