using DefaultEcs;
using UnityEngine;
using Zenject;

public class BulletEntity : MonoBehaviour
{
	private World _world;

	[SerializeField] private Transform _transform;

	[Inject]
	private void Construct(World world)
	{
		_world = world;
	}
}