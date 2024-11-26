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
		if (_inputService.LookInput.x > 0.5f)
		{
			SpawnBullet(BulletType.Normal, 10f, 25f, 5f);
		}
		else if (_inputService.LookInput.y > 0.5f)
		{
			SpawnBullet(BulletType.Explosive, 8f, 50f, 3f);
		}
		else if (_inputService.LookInput.x < -0.5f)
		{
			SpawnBullet(BulletType.Piercing, 12f, 15f, 6f);
		}
	}

	private void SpawnBullet(BulletType type, float speed, float damage, float lifetime)
	{
		Vector3 spawnPosition = transform.position + transform.forward;
		Vector3 direction = transform.forward;

		_bulletFactory.CreateBullet(type, spawnPosition, direction, speed, damage, lifetime);
	}
}