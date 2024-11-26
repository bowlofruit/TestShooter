using DefaultEcs;
using UnityEngine;

public class BulletAdapter : EntityBaseComponent<BulletComponent>
{
    [SerializeField] private float _speed;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<BulletComponent>();

		component.Speed = _speed;
	}
}