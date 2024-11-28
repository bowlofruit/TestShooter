using System.Collections.Generic;
using UnityEngine;
using DefaultEcs;
using Zenject;

public class EntityBulletPool
{
	private readonly Dictionary<BulletType, Stack<Entity>> _entityPool = new Dictionary<BulletType, Stack<Entity>>();
	private readonly Dictionary<BulletType, GameObject> _prefabs;
	private readonly Transform _poolParent;
	private readonly DiContainer _container;
	private readonly World _world;

	public EntityBulletPool(Dictionary<BulletType, GameObject> prefabs, int initialSize, Transform poolParent, DiContainer container, World world)
	{
		_prefabs = prefabs;
		_poolParent = poolParent;
		_container = container;
		_world = world;

		foreach (var bulletType in _prefabs.Keys)
		{
			_entityPool[bulletType] = new Stack<Entity>();

			for (int i = 0; i < initialSize; i++)
			{
				var entity = CreateNewEntity(bulletType);
				_entityPool[bulletType].Push(entity);
			}
		}

		LogPoolContents();
	}

	public Entity GetEntity(BulletType bulletType, Vector3 position)
	{
		if (_entityPool[bulletType].Count > 0)
		{
			var entity = _entityPool[bulletType].Pop();
			var gameObject = entity.Get<GameObjectComponent>().Value;

			gameObject.SetActive(true);
			gameObject.transform.position = position;

			Debug.Log($"EntityBulletPool: Retrieved entity for {bulletType}. Remaining in pool: {_entityPool[bulletType].Count}");
			return entity;
		}

		Debug.LogWarning($"EntityBulletPool: Pool empty for {bulletType}. Creating new entity.");
		return CreateNewEntity(bulletType, position);
	}

	public void ReturnEntity(Entity entity, BulletType bulletType)
	{
		var gameObject = entity.Get<GameObjectComponent>().Value;

		entity.Remove<SpeedComponent>();
		entity.Remove<DirectionComponent>();
		entity.Remove<DamageComponent>();
		entity.Remove<LifetimeComponent>();

		gameObject.SetActive(false);
		gameObject.transform.SetParent(_poolParent);

		_entityPool[bulletType].Push(entity);
		Debug.Log($"EntityBulletPool: Returned entity for {bulletType}. Total in pool: {_entityPool[bulletType].Count}");
	}

	private Entity CreateNewEntity(BulletType bulletType, Vector3? position = null)
	{
		var bulletPrefab = _prefabs[bulletType];
		var gameObject = _container.InstantiatePrefab(bulletPrefab, _poolParent);

		if (position.HasValue)
		{
			gameObject.transform.position = position.Value;
		}

		gameObject.SetActive(false);

		var entity = _world.CreateEntity();
		entity.Set(new GameObjectComponent { Value = gameObject });
		entity.Set(new BulletTypeComponent { Type = bulletType });

		Debug.Log($"EntityBulletPool: Created new entity for {bulletType}.");
		return entity;
	}

	public void LogPoolContents()
	{
		foreach (var kvp in _entityPool)
		{
			Debug.Log($"BulletType: {kvp.Key}, Count: {kvp.Value.Count}");
		}
	}
}
