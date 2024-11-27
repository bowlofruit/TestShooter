using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

public class PlayerMovementSystem : AEntitySetSystem<float>
{
	private readonly IInputService _inputService;

	public PlayerMovementSystem(World world, IInputService inputService) : base(world.GetEntities()
		.With<TransformComponent>()
		.With<SpeedComponent>()
		.With<PlayerTagComponent>()
		.AsSet())
	{
		_inputService = inputService;
	}

	protected override void Update(float deltatime, in Entity entity)
	{
		ref var transformComponent = ref entity.Get<TransformComponent>();
		ref var speedComponent = ref entity.Get<SpeedComponent>();
		ref var directionComponent = ref entity.Get<DirectionComponent>();

		directionComponent.Direction = _inputService.MoveInput;

		transformComponent.Transform.position += new Vector3(directionComponent.Direction.x, directionComponent.Direction.y, 0) * speedComponent.Speed * deltatime;
	}
}