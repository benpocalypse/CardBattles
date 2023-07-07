using Godot;
using System;

namespace CardBattles;

public partial class LandSpace : Node3D
{
	[Export]
	private int LandNumber = 0;

	[Signal]
	public delegate void LandSpaceEnteredEventHandler(int cardNumber);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LandSpaceEnteredEvent()
	{
		GD.Print($"LandSpace: {LandNumber} entered!");
		EmitSignal(SignalName.LandSpaceEntered, LandNumber);
	}
}
