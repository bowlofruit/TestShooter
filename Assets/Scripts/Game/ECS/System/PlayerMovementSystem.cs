using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

public class PlayerMovementSystem : AEntitySetSystem<float>
{
	private readonly IInputService _inputService;

	public PlayerMovementSystem(World world, IInputService inputService) : base(world.GetEntities()
		.With<TransformComponent>()
		.With<PlayerMovementSpeedComponent>()
		.AsSet())
	{
		_inputService = inputService;
	}

	protected override void Update(float deltatime, in Entity entity)
	{
		ref var transformComponent = ref entity.Get<TransformComponent>();
		ref var movementComponent = ref entity.Get<PlayerMovementSpeedComponent>();

		var direction = _inputService.MoveInput;

		transformComponent.Transform.position += new Vector3(direction.x, direction.y, 0) * movementComponent.Speed * deltatime;
	}
}