using Godot;
using System;

public class PlayerDetectionZone : Area2D
{
    public KinematicBody2D player = null;
    public override void _Ready()
    {

    }

    public bool IsPlayerVisible()
    {
        return player != null;
    }
    public void _on_PlayerDetectionZone_body_entered(KinematicBody2D body)
    {
        player = body;
    }

    public void _on_PlayerDetectionZone_body_exited(KinematicBody2D body)
    {
        player = null;
    }
}
