using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 110.0f;
	private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("sprite");
		_sprite.Animation = "idle";
    }
    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
			//GD.Print($"vX: {velocity.X} vY: {velocity.Y} dX: {direction.X} dY: {direction.Y}");
			changeAnimation(direction);	
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			//GD.Print($"vX: {velocity.X} vY: {velocity.Y} dX: {direction.X} dY: {direction.Y}");
			changeAnimation(direction);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void changeAnimation(Vector2 moveDirection)
	{
		if(moveDirection.X == -1f)
		{
			_sprite.Animation = "left";
		}
		if(moveDirection.X == 1f)
		{
			_sprite.Animation = "right";
		}
		if(moveDirection.Y == 1f)
		{
			_sprite.Animation = "down";
		}
		if(moveDirection.Y == -1f)
		{
			_sprite.Animation = "up";
		}
		if(moveDirection.Y == 0f && moveDirection.X == 0f)
		{
			_sprite.Animation = "idle";
		}
	}
}
