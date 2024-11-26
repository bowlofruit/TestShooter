using DefaultEcs;
using DefaultEcs.System;

public class BulletLifetimeSystem : AEntitySetSystem<float>
{
	private readonly BulletSpawner _bulletSpawner;

	public BulletLifetimeSystem(World world, BulletSpawner bulletSpawner) : base(world.GetEntities().
        With<GameObjectComponent>().
        With<LifetimeComponent>().
        With<BulletTypeComponent>()
        .AsSet())
    {
        _bulletSpawner = bulletSpawner;
    }

	protected override void Update(float deltaTime, in Entity entity)
	{
		ref var lifetime = ref entity.Get<LifetimeComponent>();
        ref var bulletType = ref entity.Get<BulletTypeComponent>();

        lifetime.RemainingTime -= deltaTime;

        if (lifetime.RemainingTime <= 0)
        {
            var bulletObject = entity.Get<GameObjectComponent>().Value;
			_bulletSpawner.ReturnBullet(bulletObject, bulletType.Type);

			entity.Dispose();
        }
	}
}
