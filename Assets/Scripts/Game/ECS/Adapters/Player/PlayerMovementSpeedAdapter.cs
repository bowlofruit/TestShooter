using DefaultEcs;
using UnityEngine;

public class PlayerMovementSpeedAdapter : EntityBaseComponent<PlayerMovementSpeedComponent>
{
    [SerializeField] private float _speed;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<PlayerMovementSpeedComponent>();

		component.Speed = _speed;
	}
}