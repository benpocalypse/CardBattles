using Godot;
using System;

public static class NumericExtensions
{
    public static float ToRadians(this float val)
    {
        return ((float)(Math.PI / 180) * val);
    }
}

public partial class GameplayBattle : Node3D
{
	private bool _mouseClicked = false;
	private Node3D _card;
	private Node3D _land1;
	private Camera3D _gameplayCamera;
	private Camera3D _actionCamera;
	private PathFollow3D _path;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_path = GetNode<SubViewportContainer>("ActionViewportContainer").GetChild<SubViewport>(0).GetChild<Path3D>(0).GetChild<PathFollow3D>(0);
		_actionCamera = GetNode<SubViewportContainer>("ActionViewportContainer").GetChild<SubViewport>(0).GetChild<Path3D>(0).GetChild<PathFollow3D>(0).GetChild<Camera3D>(0);
		_gameplayCamera = GetNode<SubViewportContainer>("GameplayViewportContainer").GetChild(0).GetChild<Camera3D>(0);
		_land1 = GetNode<Node3D>("Land1Node3d");
	}

    private void CardClicked(int cardNumber)
    {
        GD.Print($"Main scene, card {cardNumber} clicked!");

		_card = GetNode<Node3D>("Card");
		_card.Visible = true;
		_mouseClicked = true;
		/*
		_gameplayCamera.Current = false;
		GetViewport().GetCamera3D().Current = false;
		_actionCamera.Current = true;
		*/
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

		if (_mouseClicked)
		{
			if (_path.ProgressRatio < 0.95f)
			{
				//_path.ProgressRatio += 0.01f;
			}
			//GD.Print($"Camera position = {_gameplayCamera.Position.X}, {_gameplayCamera.Position.Y}, {_gameplayCamera.Position.Z}");
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		
		
		//GD.Print($"Card position = {_card.Position.X}, {_card.Position.Y}, {_card.Position.Z}");
	}

	private float _rotationX = 0f;
	private float _rotationY = 0f;
	private float LookAroundSpeed = 0.01f;
	private Vector3 _toPosition;

	public override void _Input(InputEvent @event)	
	{
		if (@event is InputEventMouseMotion mouseMotion && _mouseClicked)
		{
        	var fromPosition = _gameplayCamera.ProjectRayOrigin(mouseMotion.Position);
			//var fromPosition = _land1.GetChild(0).ProjectRayOrigin(mouseMotion.Position);
        	_toPosition = fromPosition + _gameplayCamera.ProjectRayNormal(mouseMotion.Position) * 5;

			var currentRotationDegrees = (((_land1.Position.Z - _card.Position.Z) / _land1.Position.Z) * 270.0f);
			Vector3 rotationDegrees = _card.RotationDegrees;
			rotationDegrees.Z = currentRotationDegrees;

			GD.Print($"_card.RotationDegrees.Z = {_card.RotationDegrees.Z}, _land1.Position.Z = {_land1.Position.Z}. _card.Position.Z = {_card.Position.Z}, currentRotationDegrees = {currentRotationDegrees}");
			if (_card.RotationDegrees.X < currentRotationDegrees)
			{
				_card.RotationDegrees = rotationDegrees;
				//GD.Print($"_card.Rotation.X = {_card.Rotation.X}. currentRotation = {currentRotation}");
				//_card.RotateX(_card.Rotation.X - currentRotation);
			}
			else if (_card.RotationDegrees.X > currentRotationDegrees)
			{
				_card.RotationDegrees = rotationDegrees;
				//GD.Print($"_card.Rotation.X = {_card.Rotation.X}. currentRotation = {currentRotation}");
				//_card.RotateX(_card.Rotation.X + currentRotation);
			}

			var newPosition = new Vector3(_toPosition.X, _card.Position.Y, _toPosition.Z);

			_card.Position = newPosition;

			//GD.Print($"mousePosition.X = {mouseMotion.Position.X}, mousePosition.Y = {mouseMotion.Position.Y}");
		}
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
