using DefaultEcs;
using UnityEngine;
using Zenject;

public class EntityBootsrtapper : MonoBehaviour
{
	[SerializeField] private EntityBase _entityBase;

	[Header("For Debug")] 
	public int EntityId;

	private World _world;

	[Inject]
	private void Construct(World world)
	{
		_world = world;
	}

	private void OnEnable()
	{
		var entity = _entityBase.CreateEntity(_world);

		EntityId = entity.GetHashCode();
	}

	private void OnDisable()
	{
		_entityBase.Entity.Dispose();
	}
}
