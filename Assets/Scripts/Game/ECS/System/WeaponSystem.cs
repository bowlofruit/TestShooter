using UnityEngine;
using DefaultEcs.System;
using DefaultEcs;

public class WeaponSystem : AEntitySetSystem<float>
{
	private readonly IBulletFactory _bulletFactory;
	private readonly IInputService _inputService;
	private readonly World _world;
	private Entity _playerEntity;

	public WeaponSystem(World world, IBulletFactory bulletFactory, IInputService inputService) : base (world.GetEntities()
		.With<WeaponTagComponent>()
		.AsSet())
	{
		_world = world;
		_bulletFactory = bulletFactory;
		_inputService = inputService;
	}

	protected override void Update(float deltaTime, in Entity entity)
	{
		if (_playerEntity == default)
		{
			var playerEntities = _world.GetEntities().With<PlayerTagComponent>().AsSet();
			var enumerator = playerEntities.GetEntities().GetEnumerator();

			_playerEntity = enumerator.MoveNext() ? enumerator.Current : default;
			return;
		}

		int shootInput = _inputService.ShootInput;
		switch (shootInput)
		{
			case 1:
				SpawnBullet(BulletType.Normal, 10f, 25f, 5f);
				break;
			case 2:
				SpawnBullet(BulletType.Explosive, 8f, 50f, 3f);
				break;
			case 3:
				SpawnBullet(BulletType.Piercing, 12f, 15f, 6f);
				break;
		}
	}

	private void SpawnBullet(BulletType type, float speed, float damage, float lifetime)
	{
		Vector3 spawnPosition = _playerEntity.Get<TransformComponent>().Transform.position;
		Vector3 direction = _playerEntity.Get<DirectionComponent>().Direction;

		_bulletFactory.CreateBullet(type, spawnPosition, direction, speed, damage, lifetime);
	}
}