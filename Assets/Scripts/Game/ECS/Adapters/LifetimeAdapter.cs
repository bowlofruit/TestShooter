using DefaultEcs;
using UnityEngine;

namespace Assets.Spripts.Game.ECS.Adapters.Bullet
{
	public class LifetimeAdapter : EntityBaseComponent<LifetimeComponent>
	{
		[SerializeField] private float _lifetime;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref entity.Get<LifetimeComponent>();

			component.RemainingTime = _lifetime;
		}
	}
}