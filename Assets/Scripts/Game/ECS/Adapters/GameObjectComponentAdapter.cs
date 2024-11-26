using DefaultEcs;
using UnityEngine;

public class GameObjectComponentAdapter : EntityBaseComponent<GameObjectComponent>
{
	[SerializeField] private GameObject _gameObject;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref entity.Get<GameObjectComponent>();
		component.Value = _gameObject;
	}
}
