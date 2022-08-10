using Godot;
using System;

public class NetworkPlayerSpawner : Node
{
	PackedScene player = GD.Load<PackedScene>("PlayerCharacter.tscn");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Ready");
		GetTree().Connect("network_peer_connected", this, "PlayerConnectedCallback");
		GetTree().Connect("network_peer_disconnected", this, "PlayerDisconnectedCallback");
		
		if(GetTree().NetworkPeer != null) {
			// Global.EmitSignal("toggle_network_setup")
		}
	}
	
	private void InstancePlayer(int id) {
		PlayerController playerInstance = player.Instance() as PlayerController;
		playerInstance.SetNetworkMaster(GetTree().GetNetworkUniqueId());
		playerInstance.Name = GetTree().GetNetworkUniqueId().ToString();

		Transform t = playerInstance.Transform;
		t.origin = new Vector3(0, 5, 0);
		playerInstance.Transform = t;

		AddChild(playerInstance);
	}
	
	private void PlayerConnectedCallback(int id) {
		GD.Print("Instancing player object for player: " + id.ToString());
		InstancePlayer(id);
	}
	
	private void PlayerDisonnectedCallback(int id) {
		GD.Print("Player disconnected: " + id.ToString());
		if(HasNode(id.ToString())) {
			GetNode(id.ToString()).QueueFree();
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
