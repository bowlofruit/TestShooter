using System.Collections.Generic;
using UnityEngine;

public class BulletPool 
{
    private readonly Dictionary<BulletType, Stack<GameObject>> _poolDict = new Dictionary<BulletType, Stack<GameObject>>();
    private readonly Dictionary<BulletType, GameObject> _prefabs;
    private readonly Transform _poolParent;

	public BulletPool(Dictionary<BulletType, GameObject> prefabs, int initSize, Transform poolParent)
	{
		// Log if prefabs are missing or empty
		if (prefabs == null || prefabs.Count == 0)
		{
			Debug.LogError("Prefabs dictionary is null or empty.");
			return;
		}

		_prefabs = prefabs;
		_poolParent = poolParent;

		foreach (var bulletType in prefabs.Keys)
		{
			_poolDict[bulletType] = new Stack<GameObject>();

			for (int i = 0; i < initSize; i++)
			{
				Debug.Log($"Instantiating bullet prefab for {bulletType}");
				var bullet = Object.Instantiate(_prefabs[bulletType], _poolParent);
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
            return bullet;
        }

		var newBullet = Object.Instantiate(_prefabs[type], _poolParent);
		return newBullet;
	}

    public void ReturnBullet(GameObject bullet, BulletType type)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(_poolParent);
        _poolDict[type].Push(bullet);
    }
}
