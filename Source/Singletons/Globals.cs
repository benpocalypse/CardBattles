using Godot;
using System;
using OneOf;

namespace ng.bengineeri.CardBattles;

public partial class Globals : Node
{
	static Globals()
	{
	}

	private Globals()
	{
	}

	private static Globals _instance;
	public static Globals Instance
	{
		get
		{
			return _instance;
		}
	}

	public Node CurrentSceneFile { get; set; }

	public record Scenes
	{
		public record Titlescreen;
		public record Gameplay;
	};

	private OneOf<Scenes.Titlescreen, Scenes.Gameplay> _currentScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Viewport root = GetTree().Root;
		CurrentSceneFile = root.GetChild(root.GetChildCount() - 1);

		_instance = GetNode<Globals>("/root/Globals");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GotoScene(string nextScene)//(Scenes nextScene)
	{
		// This function will usually be called from a signal callback,
		// or some other function from the current scene.
		// Deleting the current scene at this point is
		// a bad idea, because it may still be executing code.
		// This will result in a crash or unexpected behavior.

		// The solution is to defer the load to a later time, when
		// we can be sure that no code from the current scene is running:
		CallDeferred(nameof(DeferredGotoScene), "res://Scenes/" + nextScene + ".tscn");
	}

	public void DeferredGotoScene(string path)
	{
		// It is now safe to remove the current scene
		CurrentSceneFile.Free();

		// Load a new scene.
		var nextScene = (PackedScene)GD.Load(path);

		// Instance the new scene.
		CurrentSceneFile = nextScene.Instantiate();

		// Add it to the active scene, as child of root.
		GetTree().Root.AddChild(CurrentSceneFile);

		// Optionally, to make it compatible with the SceneTree.change_scene() API.
		GetTree().CurrentScene = CurrentSceneFile;
	}
}
