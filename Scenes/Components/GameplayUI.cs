using Godot;
using System;

namespace ng.bengineeri.CardBattles;

public partial class GameplayUI : Control
{
	[Export]
	private int TestExport = 3;

	[Signal]
	public delegate void CardClickedEventHandler(int cardNumber);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Ready!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Card1_button_pressed()
	{
		GD.Print("Card 1 pressed!");
		GetNode<TextureButton>("Card1").Visible = false;
		EmitSignal(SignalName.CardClicked, 1);
	}

	public void Card2_button_pressed()
	{
		GD.Print("Card 2 pressed!");
		GetNode<TextureButton>("Card2").Visible = false;
		EmitSignal(SignalName.CardClicked, 2);
	}

	public void Card3_button_pressed()
	{
		GD.Print("Card 3 pressed!");
		GetNode<TextureButton>("Card3").Visible = false;
		EmitSignal(SignalName.CardClicked, 3);
	}
}
