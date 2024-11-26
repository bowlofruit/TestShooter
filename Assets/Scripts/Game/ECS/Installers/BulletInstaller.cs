using DefaultEcs;
using System.Collections.Generic;
using System.ComponentModel;
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

	[Inject]
	private void Construct(World world)
	{
		_world = world;
	}

	public override void InstallBindings()
	{
		var bulletPrefabs = new Dictionary<BulletType, GameObject>
		{
			{ BulletType.Normal, _normalBulletPrefab },
			{ BulletType.Explosive, _explosiveBulletPrefab },
			{ BulletType.Piercing, _piercingBulletPrefab }
		};

		var bulletPool = new BulletPool(bulletPrefabs, _poolSize, _poolParent);
		Container.Bind<BulletPool>().FromInstance(bulletPool).AsSingle();

		Container.Bind<IBulletFactory>().To<BulletSpawner>().AsSingle()
			.WithArguments(bulletPool, _world);
	}
}
