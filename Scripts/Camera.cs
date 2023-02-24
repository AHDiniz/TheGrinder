using Godot;
using System;

public class Camera : Camera2D
{
    private Node2D _player;

    public override void _Ready()
    {
        _player = GetTree().Root.GetNode<Node2D>("World").GetNode<KinematicBody2D>("Player");
    }

    public override void _Process(float delta)
    {
        GlobalPosition = _player.GlobalPosition;
    }
}
