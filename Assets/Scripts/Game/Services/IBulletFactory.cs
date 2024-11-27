using UnityEngine;

public interface IBulletFactory
{
	public GameObject CreateBullet(BulletType type, Vector3 position, Vector3 direction, float speed, float damage, float lifetime);
	public void ReturnBullet(GameObject bulletObject, BulletType type);
}
