using DefaultEcs;
using UnityEngine;

public class DirectionAdapter : EntityBaseComponent<DirectionComponent>
{
	[SerializeField] private Vector3 _direction;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<DirectionComponent>();

		component.Direction = _direction;
	}
}