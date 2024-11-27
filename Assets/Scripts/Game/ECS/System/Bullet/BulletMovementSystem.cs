using DefaultEcs;
using DefaultEcs.System;

public class BulletMovementSystem : AEntitySetSystem<float>
{
	private readonly IInputService _inputService;

	public BulletMovementSystem(World world, IInputService inputService) : base(world.GetEntities()
		.With<BulletTypeComponent>()
		.AsSet())
	{
		_inputService = inputService;
	}

	protected override void Update(float deltatime, in Entity entity)
	{
		ref var transformComponent = ref entity.Get<TransformComponent>();
		ref var speedComponent = ref entity.Get<SpeedComponent>();
		ref var directionComponent = ref entity.Get<DirectionComponent>();

        transformComponent.Transform.position += speedComponent.Speed * deltatime * directionComponent.Direction;
	}
}