using Godot;
using System;

public partial class GameplayUI : Control
{
	[Export]
	private int TestExport = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Ready!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Card1_on_area_2d_input_event(Viewport viewport, InputEvent _event, int shape_idx)
	{
		GD.Print("Clicked!");
	}

	public void _on_area_2d_mouse_entered()
	{
		GD.Print("Mouse entered1!");
	}

	public void _on_texture_rect_gui_input()
	{
		GD.Print("Mouse entered2!");
	}

	public void Area2d_on_area_2d_area_entered()
	{
		GD.Print("Mouse entered3!");
	}

	public void Card2_button_pressed()
	{
		GD.Print("Card 2 pressed!");
	}
}
