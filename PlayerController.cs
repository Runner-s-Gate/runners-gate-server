using Godot;

public class PlayerController : KinematicBody
{
	private Vector3 velocity = new Vector3();

	private Label NameLabel { get; set; }

	[Puppet]
	public Vector3 PuppetPosition { get; set; }
	[Puppet]
	public Vector3 PuppetVelocity { get; set; }

	public override void _PhysicsProcess(float delta)
	{
		Transform t = Transform;
		t.origin = PuppetPosition;
		Transform = t;
			
		velocity = PuppetVelocity;
		velocity = MoveAndSlide(velocity);
	}

	public void SetPlayerName(string name)
	{
		NameLabel = (Label)GetNode("LabelOrigin/Viewport/Label");
		NameLabel.Text = name;
	}
}
