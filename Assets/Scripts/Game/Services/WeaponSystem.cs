using Zenject;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
	private IBulletFactory _bulletFactory;
	private IInputService _inputService;

	[Inject]
	public void Construct(IBulletFactory bulletFactory, IInputService inputService)
	{
		_bulletFactory = bulletFactory;
		_inputService = inputService;
	}

	void Update()
	{
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
		Vector3 spawnPosition = transform.position + transform.forward;
		Vector3 direction = transform.forward;

		_bulletFactory.CreateBullet(type, spawnPosition, direction, speed, damage, lifetime);
	}
}