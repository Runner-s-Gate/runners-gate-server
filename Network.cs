using Godot;
using System;
using System.Collections.Generic;

public class Network : Node
{
	private static Network _instance;
	
	private readonly int defaultPort = (int)ConfigLoader.GetValue("network", "server_port");

	private string PlayerName { get; set; }

	private Dictionary<int, string> Players = new Dictionary<int, string>();

	public static Network GetInstance() {
		return _instance;
	}

	public override void _Ready()
	{
		GetTree().Connect("network_peer_connected", this, nameof(PlayerConnected));
		GetTree().Connect("network_peer_disconnected", this, nameof(PlayerDisconnected));
		
		_instance = this;
	}

	public void HostGame()
	{
		var peer = new NetworkedMultiplayerENet();
		peer.CreateServer(defaultPort, 32);
		GetTree().NetworkPeer = peer;
		
		GD.Print("You are now hosting on:");
		GD.Print(" - PORT: " + defaultPort);
	}

	private void PlayerConnected(int id)
	{
		GD.Print($"Player connected: " + id);
		// tell the player that just connected who we are by sending an rpc back to them with your name.
		RpcId(id, nameof(RegisterPlayer), PlayerName);
	}

	private void PlayerDisconnected(int id)
	{
		GD.Print($"Player disconnected: {id}");
		RemovePlayer(id);
	}

	[Remote]
	private void RegisterPlayer(string playerName)
	{
		var id = GetTree().GetRpcSenderId();
		Players.Add(id, playerName);
		GD.Print($"{playerName} added with ID {id}");
		SpawnPlayer(id, playerName);
	}

	private void SpawnPlayer(int id, string playerName)
	{
		GD.Print($"SpawnPlayer()");
		// load the players
		var playerScene = (PackedScene)ResourceLoader.Load("res://PlayerCharacter.tscn");

		var playerNode = (PlayerController)playerScene.Instance();
		playerNode.Name = id.ToString();
		playerNode.SetNetworkMaster(id);

		Transform t = playerNode.Transform;
		t.origin = new Vector3(0, 5, 0);
		playerNode.Transform = t;

		playerNode.SetPlayerName(GetTree().GetNetworkUniqueId() == id ? PlayerName : playerName);

		AddChild(playerNode);
	}

	[Remote]
	private void RemovePlayer(int id)
	{
		GD.Print($"RemovePlayer()");
		if (Players.ContainsKey(id))
		{
			Players.Remove(id);
			GetNode(id.ToString()).QueueFree();
		}
	}
	
}
