using UnityEngine;

public interface IBulletFactory
{
	GameObject CreateBullet(BulletType type, Vector3 position, Vector3 direction, float speed, float damage, float lifetime);
}
