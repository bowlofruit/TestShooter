using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletPool 
{
    private readonly Dictionary<BulletType, Stack<GameObject>> _poolDict = new Dictionary<BulletType, Stack<GameObject>>();
    private readonly Dictionary<BulletType, GameObject> _prefabs;
    private readonly Transform _poolParent;
	private readonly DiContainer _container;

	public BulletPool(Dictionary<BulletType, GameObject> prefabs, int initSize, Transform poolParent, DiContainer container)
	{
		_prefabs = prefabs;
		_poolParent = poolParent;
		_container = container;

		foreach (var bulletType in prefabs.Keys)
		{
			_poolDict[bulletType] = new Stack<GameObject>();

			for (int i = 0; i < initSize; i++)
			{
				Debug.Log($"Instantiating bullet prefab for {bulletType}");
				var bullet = _container.InstantiatePrefab(_prefabs[bulletType], _poolParent);
				bullet.SetActive(false);
				_poolDict[bulletType].Push(bullet);
			}
		}
	}


	public GameObject GetBullet(BulletType type)
    {
        if (_poolDict[type].Count > 0)
        {
            var bullet = _poolDict[type].Pop();
            bullet.SetActive(true);
			_container.Inject(bullet);
            return bullet;
        }

		var newBullet = _container.InstantiatePrefab(_prefabs[type], _poolParent);
		_container.Inject(newBullet);
		return newBullet;
	}

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(_poolParent);
        _poolDict[type].Push(bullet);
    }
}
