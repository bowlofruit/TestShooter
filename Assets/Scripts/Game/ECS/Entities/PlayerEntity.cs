using DefaultEcs;
using UnityEngine;
using Zenject;

public class PlayerEntity : MonoBehaviour
{
	private World _world;

    [SerializeField] private Transform _transform;
	[SerializeField] private float _rotationSpeed;

	[Inject]
	private void Construct(World world)
	{
		_world = world;
	}
}
