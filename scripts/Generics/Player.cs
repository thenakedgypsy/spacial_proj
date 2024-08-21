using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 70.0f;
    private AnimatedSprite2D _sprite;
    private Vector2 _currentDirection = Vector2.Zero;
    private double _lastVerticalPressTime = 0;
    private double _lastHorizontalPressTime = 0;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("sprite");
        _sprite.Animation = "idle";
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        HandleInput(delta);

        if (_currentDirection != Vector2.Zero)
        {
            velocity = _currentDirection * Speed;
            changeAnimation(_currentDirection);
        }
        else
        {
            velocity = Vector2.Zero;
            changeAnimation(Vector2.Zero);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleInput(double delta)
{
    bool upPressed = Input.IsActionJustPressed("ui_up");
    bool downPressed = Input.IsActionJustPressed("ui_down");
    bool leftPressed = Input.IsActionJustPressed("ui_left");
    bool rightPressed = Input.IsActionJustPressed("ui_right");

    bool upHeld = Input.IsActionPressed("ui_up");
    bool downHeld = Input.IsActionPressed("ui_down");
    bool leftHeld = Input.IsActionPressed("ui_left");
    bool rightHeld = Input.IsActionPressed("ui_right");

    if (upPressed || downPressed)
    {
        _lastVerticalPressTime = Time.GetTicksMsec();
        _currentDirection.Y = upPressed ? -1 : 1;
        _currentDirection.X = 0;
    }
    else if (leftPressed || rightPressed)
    {
        _lastHorizontalPressTime = Time.GetTicksMsec();
        _currentDirection.X = leftPressed ? -1 : 1;
        _currentDirection.Y = 0;
    }

    // Check if the most recent direction is no longer held
    if (_lastVerticalPressTime > _lastHorizontalPressTime)
    {
        if (!upHeld && !downHeld)
        {
            // Revert to horizontal direction if it's held
            if (leftHeld || rightHeld)
            {
                _currentDirection.X = leftHeld ? -1 : 1;
                _currentDirection.Y = 0;
                _lastHorizontalPressTime = Time.GetTicksMsec();
            }
            else
            {
                _currentDirection = Vector2.Zero;
            }
        }
    }
    else
    {
        if (!leftHeld && !rightHeld)
        {
            // Revert to vertical direction if it's held
            if (upHeld || downHeld)
            {
                _currentDirection.Y = upHeld ? -1 : 1;
                _currentDirection.X = 0;
                _lastVerticalPressTime = Time.GetTicksMsec();
            }
            else
            {
                _currentDirection = Vector2.Zero;
            }
        }
    }
}

    public void changeAnimation(Vector2 moveDirection)
    {
        if (moveDirection.X == -1f)
        {
            _sprite.Animation = "left";
        }
        else if (moveDirection.X == 1f)
        {
            _sprite.Animation = "right";
        }
        else if (moveDirection.Y == 1f)
        {
            _sprite.Animation = "down";
        }
        else if (moveDirection.Y == -1f)
        {
            _sprite.Animation = "up";
        }
        else
        {
            _sprite.Animation = "idle";
        }
    }
}