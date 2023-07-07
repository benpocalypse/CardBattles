using Godot;
using System;

namespace CardBattles;

public partial class GameplayBattle : Node3D
{
	private bool _mouseClicked = false;
	private bool _rightButtonClicked = false;
	private Node3D _card;
	private Node3D _cardPivotPoint;
	private Camera3D _gameplayCamera;
	private SubViewportContainer _gameplayViewport;
	private Camera3D _actionCamera;
	private SubViewportContainer _actionViewport;
	private PathFollow3D _path;
	private Vector3 _toPosition;
	private GameplayUI _gameplayUi;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_path = GetNode<SubViewportContainer>("ActionViewportContainer").GetChild<SubViewport>(0).GetChild<Path3D>(0).GetChild<PathFollow3D>(0);
		_actionCamera = GetNode<SubViewportContainer>("ActionViewportContainer").GetChild<SubViewport>(0).GetChild<Path3D>(0).GetChild<PathFollow3D>(0).GetChild<Camera3D>(0);
		_actionViewport = GetNode<SubViewportContainer>("ActionViewportContainer");
		_gameplayCamera = GetNode<SubViewportContainer>("GameplayViewportContainer").GetChild(0).GetChild<Camera3D>(0);
		_gameplayViewport = GetNode<SubViewportContainer>("GameplayViewportContainer");
		_gameplayUi = this.GetParent().GetNode<GameplayUI>("UserInterface");
		_cardPivotPoint = GetNode<Node3D>("CardPivotPoint");
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (_rightButtonClicked)
		{
			_gameplayCamera.Current = false;
			_gameplayViewport.Visible = false;
			_actionViewport.Visible = true;
			_actionCamera.Current = true;
            _gameplayUi.Visible = false;

			if (_path.ProgressRatio < 0.95f)
			{
				_path.ProgressRatio += 0.01f;
			}
		}

		if (Input.IsActionPressed("ui_right"))
		{
			TranslateObjectLocal(-Transform.Basis.X);
		}

		if (Input.IsActionPressed("ui_left"))
		{
			TranslateObjectLocal(Transform.Basis.X);
		}

		if (Input.IsActionPressed("ui_up"))
		{
			TranslateObjectLocal(Transform.Basis.Z);
		}

		if (Input.IsActionPressed("ui_down"))
		{
			TranslateObjectLocal(-Transform.Basis.Z);
		}
	}


	public override void _Input(InputEvent @event)	
	{
		if (@event is InputEventMouseMotion mouseMotion && _mouseClicked)
		{
        	var fromPosition = _gameplayCamera.ProjectRayOrigin(mouseMotion.Position);
        	_toPosition = fromPosition + _gameplayCamera.ProjectRayNormal(mouseMotion.Position) * 5;

			var currentRotationDegrees = (((_cardPivotPoint.Position.Z - _card.Position.Z) / _cardPivotPoint.Position.Z) * 270.0f);
			Vector3 rotationDegrees = _card.RotationDegrees;
			rotationDegrees.Z = currentRotationDegrees;

			if (_card.RotationDegrees.X < currentRotationDegrees)
			{
				_card.RotationDegrees = rotationDegrees;
			}
			else if (_card.RotationDegrees.X > currentRotationDegrees)
			{
				_card.RotationDegrees = rotationDegrees;
			}

			var newPosition = new Vector3(_toPosition.X, _card.Position.Y, _toPosition.Z);
			_card.Position = newPosition;
		}

		if(@event is InputEventMouseButton mouseButton && mouseButton.Pressed)
		{
			_rightButtonClicked = mouseButton.ButtonIndex switch
			{
				MouseButton.Right => true,
				_ => false
			};

		}
	}

	private void CardClicked(int cardNumber)
    {
        GD.Print($"Main scene, card {cardNumber} clicked!");

		_card = GetNode<Node3D>("Card");
		_card.Visible = true;
		_mouseClicked = true;
    }

	private void Land1AreaEntered(Area3D area)
	{
		GD.Print("Area entered!");

		Vector3 rotationDegrees = _card.RotationDegrees;
		rotationDegrees.Z = _cardPivotPoint.Rotation.Z;

		_card.RotationDegrees = rotationDegrees;
		_card.Position = _cardPivotPoint.Position;
		_mouseClicked = false;
	}

	private void OnLandSpaceEntered(int landNumber)
	{
		GD.Print($"New Land Space {landNumber} Entered!");

		var landNode = GetNode<Node3D>($"LandSpace{landNumber}");

		Vector3 rotationDegrees = _card.RotationDegrees;
		rotationDegrees.Z = landNode.Rotation.Z;

		_card.RotationDegrees = rotationDegrees;
		_card.Position = landNode.Position;
		_mouseClicked = false;
	}
}
