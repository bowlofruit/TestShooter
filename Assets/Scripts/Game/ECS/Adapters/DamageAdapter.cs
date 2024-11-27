using DefaultEcs;
using UnityEngine;

public class DamageAdapter : EntityBaseComponent<DamageComponent>
{
    [SerializeField] private float _damage;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<DamageComponent>();

		component.Damage = _damage;
	}
}