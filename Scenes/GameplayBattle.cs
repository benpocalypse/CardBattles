using Godot;
using System;

public partial class GameplayBattle : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			//var cam = GetNodeOrNull<Camera3D>("Camera");
			TranslateObjectLocal(-Transform.Basis.X);
		}

		if (Input.IsActionPressed("ui_left"))
		{
			//var cam = GetNodeOrNull<Camera3D>("Camera");
			TranslateObjectLocal(Transform.Basis.X);
		}

		if (Input.IsActionPressed("ui_up"))
		{
			//var cam = GetNodeOrNull<Camera3D>("Camera");
			TranslateObjectLocal(Transform.Basis.Z);
		}

		if (Input.IsActionPressed("ui_down"))
		{
			//var cam = GetNodeOrNull<Camera3D>("Camera");
			TranslateObjectLocal(-Transform.Basis.Z);
		}
	}

	private float _rotationX = 0f;
	private float _rotationY = 0f;
	private float LookAroundSpeed = 0.01f;

	public override void _Input(InputEvent @event)	
	{
		/*
		if (@event is InputEventMouseMotion mouseMotion)
		{
			// modify accumulated mouse rotation
			_rotationX += mouseMotion.Relative.X * LookAroundSpeed;
			_rotationY += mouseMotion.Relative.Y * LookAroundSpeed;

			// reset rotation
			Transform3D transform = Transform;
			transform.Basis = Basis.Identity;
			Transform = transform;

			RotateObjectLocal(Vector3.Up, _rotationX); // first rotate about Y
			RotateObjectLocal(Vector3.Right, _rotationY); // then rotate about X
		}
		*/
	}
}