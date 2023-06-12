using Godot;
using System;
using static ng.bengineeri.CardBattles.Globals;

namespace ng.bengineeri.CardBattles;

public partial class Titlescreen : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnButtonUp()
	{
		Globals g = Globals.Instance;
		g.GotoScene("GameplayBattle3d");
	}
}
