using DefaultEcs;
using UnityEngine;
using Zenject;

public class BulletFactory : IBulletFactory
{
	private readonly BulletPool _bulletPool;
	private readonly World _world;

	[Inject]
	public BulletFactory(BulletPool bulletPool, World world)
	{
		_bulletPool = bulletPool;
		_world = world;
	}

	public GameObject CreateBullet(BulletType bulletType, Vector3 position, Vector3 direction, float speed, float damage, float lifetime)
	{
		GameObject bulletObject = _bulletPool.GetBullet(bulletType);
		bulletObject.transform.position = position;

		var entity = _world.CreateEntity();
		entity.Set(new GameObjectComponent { Value = bulletObject });
		entity.Set(new SpeedComponent { Speed = speed });
		entity.Set(new DirectionComponent { Direction = direction });
		entity.Set(new DamageComponent { Damage = damage });
		entity.Set(new LifetimeComponent { RemainingTime = lifetime });
		entity.Set(new BulletTypeComponent { Type = bulletType });

		return bulletObject;
	}

	public void ReturnBullet(GameObject bulletObject, BulletType type)
	{
		_bulletPool.ReturnBullet(bulletObject, type);
	}
}