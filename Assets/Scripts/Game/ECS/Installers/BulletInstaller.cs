using DefaultEcs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
	[SerializeField] private GameObject _normalBulletPrefab;
	[SerializeField] private GameObject _explosiveBulletPrefab;
	[SerializeField] private GameObject _piercingBulletPrefab;
	[SerializeField] private int _poolSize = 50;
	[SerializeField] private Transform _poolParent;

	private World _world;

	public override void InstallBindings()
	{
		_world = new World();

		var bulletPrefabs = new Dictionary<BulletType, GameObject>
		{
			{ BulletType.Normal, _normalBulletPrefab },
			{ BulletType.Explosive, _explosiveBulletPrefab },
			{ BulletType.Piercing, _piercingBulletPrefab }
		};

		var bulletPool = new EntityBulletPool(bulletPrefabs, _poolSize, _poolParent, Container, _world);
		Container.Bind<EntityBulletPool>().FromInstance(bulletPool).AsSingle();

		Container.Bind<IBulletFactory>().To<EntityBulletFactory>().AsSingle()
			.WithArguments(bulletPool, _world);
	}
}
