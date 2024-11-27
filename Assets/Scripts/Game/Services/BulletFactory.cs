using DefaultEcs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletFactory : IBulletFactory
{
	private readonly BulletPool _bulletPool;
	private readonly World _world;
	private readonly Dictionary<BulletType, Entity> _bulletEntities = new Dictionary<BulletType, Entity>();

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

		if (!_bulletEntities.ContainsKey(bulletType))
		{
			var entity = _world.CreateEntity();
			entity.Set(new GameObjectComponent { Value = bulletObject });
			_bulletEntities[bulletType] = entity;
		}

		var bulletEntity = _bulletEntities[bulletType];

		bulletEntity.Set(new SpeedComponent { Speed = speed });
		bulletEntity.Set(new DirectionComponent { Direction = direction });
		bulletEntity.Set(new DamageComponent { Damage = damage });
		bulletEntity.Set(new LifetimeComponent { RemainingTime = lifetime });
		bulletEntity.Set(new BulletTypeComponent { Type = bulletType });

		return bulletObject;
	}

	public void ReturnBullet(GameObject bulletObject, BulletType type)
	{
		_bulletPool.ReturnBullet(bulletObject, type);
	}
}