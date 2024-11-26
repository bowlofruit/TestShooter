using DefaultEcs;
using DefaultEcs.System;
using UnityEngine;

public class BulletMovementSystem : AEntitySetSystem<float>
{
	private readonly IInputService _inputService;

	public BulletMovementSystem(World world, IInputService inputService) : base(world.GetEntities()
		.With<TransformComponent>()
		.With<BulletComponent>()
		.AsSet())
	{
		_inputService = inputService;
	}

	protected override void Update(float deltatime, in Entity entity)
	{
		ref var transformComponent = ref entity.Get<TransformComponent>();
		ref var bullet = ref entity.Get<BulletComponent>();

		transformComponent.Transform.position += bullet.Direction * bullet.Speed * deltatime;
	}
}