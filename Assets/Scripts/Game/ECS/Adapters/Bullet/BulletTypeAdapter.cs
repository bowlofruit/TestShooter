using DefaultEcs;
using UnityEngine;

public class BulletTypeAdapter : EntityBaseComponent<BulletTypeComponent>
{
	[SerializeField] private BulletType _bulletType;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref entity.Get<BulletTypeComponent>();
		component.Type = _bulletType;
	}
}
