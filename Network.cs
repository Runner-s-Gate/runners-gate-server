using Godot;
using System;

public class Network : Node
{
	private const int DEFAULT_PORT = 8080;
	private const int MAX_CLIENTS = 6;
	
	private NetworkedMultiplayerENet Server = null;
	private NetworkedMultiplayerENet Client = null;
	
	private static Network instance = null;

	//private string ipAddr = "127.0.0.1";

	public static Network GetInstance() {
		return instance;
	}

	public override void _Ready()
	{
		GetTree().Connect("connected_to_server", this, "ConnectedToServerCallback");
		GetTree().Connect("server_disconnected", this, "ServerDisconnectedCallback");
		GetTree().Connect("connection_failed", this, "ConnectionFailedCallback");
		GetTree().Connect("network_peer_connected", this, "PlayerConnectedCallback");
		instance = this;
	}

	public void CreateServer() {
		GD.Print("Creating server object...");
		this.Server = new NetworkedMultiplayerENet();
		this.Server.CreateServer(DEFAULT_PORT, MAX_CLIENTS);
		GetTree().SetNetworkPeer(this.Server);
	}

	public void JoinServer(string ipAddr) {
		GD.Print("Creating client object...");
		Client = new NetworkedMultiplayerENet();
		Client.CreateClient(ipAddr, DEFAULT_PORT);
		GetTree().SetNetworkPeer(Client);
	}

	private void ConnectedToServerCallback() {
		GD.Print("Successfully connected to the server");
	}
	
	private void ServerDisconnectedCallback() {
		GD.Print("Disconnected from the server");
		ResetNetworkConnection();
	}

	private void ConnectionFailedCallback() {
		GD.Print("Connection to the server failed");
	}

	private void PlayerConnectedCallback(int id) {
		GD.Print("Player connected: " + id);

	}

	private void ResetNetworkConnection() {
		if(GetTree().HasNetworkPeer()) {
			GetTree().SetNetworkPeer(null);
		}
	}


}
