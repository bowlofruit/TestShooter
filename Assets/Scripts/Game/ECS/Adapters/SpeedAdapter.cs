using DefaultEcs;
using UnityEngine;

public class SpeedAdapter : EntityBaseComponent<SpeedComponent>
{
	[SerializeField] private float _speed;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<SpeedComponent>();

		component.Speed = _speed;
	}
}
