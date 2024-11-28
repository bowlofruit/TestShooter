using DefaultEcs;
using UnityEngine;
using Zenject;
using System.Linq;

public class EntityBulletFactory : IBulletFactory
{
	private readonly EntityBulletPool _bulletPool;
	private readonly World _world;

	[Inject]
	public EntityBulletFactory(EntityBulletPool bulletPool, World world)
	{
		_bulletPool = bulletPool;
		_world = world;
	}

	public GameObject CreateBullet(BulletType bulletType, Vector3 position, Vector3 direction, float speed, float damage, float lifetime)
	{
		var entity = _bulletPool.GetEntity(bulletType, position);

		entity.Set(new SpeedComponent { Speed = speed });
		entity.Set(new DirectionComponent { Direction = direction });
		entity.Set(new DamageComponent { Damage = damage });
		entity.Set(new LifetimeComponent { RemainingTime = lifetime });

		return entity.Get<GameObjectComponent>().Value;
	}

	public void ReturnBullet(GameObject bulletObject, BulletType type)
	{
		var entities = _world.GetEntities().With<GameObjectComponent>().AsSet().GetEntities();

		Entity? matchingEntity = null;
		foreach (var entity in entities)
		{
			if (entity.Get<GameObjectComponent>().Value == bulletObject)
			{
				matchingEntity = entity;
				break;
			}
		}

		if (matchingEntity.HasValue)
		{
			_bulletPool.ReturnEntity(matchingEntity.Value, type);
		}
		else
		{
			Debug.LogError($"ReturnBullet: No entity found for the provided bulletObject.");
		}
	}

}