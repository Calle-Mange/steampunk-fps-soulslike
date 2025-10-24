using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
    [ExportSubgroup("Camera settings")]
    [Export] private Camera3D Camera;
    [Export] private Node3D CameraPivot;
    [Export] private float CameraSensitivty = 0.006f;

    [ExportSubgroup("Movement settings")]
    [Export] private float Speed = 5.0f;
    [Export] private float JumpSpeed = 4.5f;
    [Export] private float Gravity = 9.8f;

    [ExportSubgroup("Components")]
    [Export] private AnimationComponent AnimationComponent;
    [Export] private Node3D ModelComponent;

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        AnimationComponent.PlayAniamtion("player_rig_idle");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        Velocity = CalculatePlayerMovement(velocity, (float)delta);
        MoveAndSlide();
    }

    public Vector3 CalculatePlayerMovement(Vector3 velocity, float delta)
    {
        if (!IsOnFloor())
        {
            velocity.Y -= Gravity * delta;
        }

        if (Input.IsActionJustPressed("jump") && IsOnFloor()) 
        {
            velocity.Y = JumpSpeed;
        }

        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        Vector3 direction = (CameraPivot.GlobalBasis * new Vector3(inputDirection.X, 0, inputDirection.Y)).Normalized();

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
            AnimationComponent.PlayAniamtion("player_rig_walk");
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
        }

        return velocity;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            CameraPivot.RotateY(-mouseMotion.Relative.X * CameraSensitivty);
            Camera.RotateX(-mouseMotion.Relative.Y * CameraSensitivty);

            Vector3 cameraRotation = Camera.Rotation;
            cameraRotation.X = Mathf.Clamp(cameraRotation.X, -80f, 80f);
            Camera.Rotation = cameraRotation;

        }

        if (@event is InputEventKey keyEvent && keyEvent.IsPressed() && keyEvent.Keycode == Key.Escape)
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
    }
}
