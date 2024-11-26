using DefaultEcs;
using UnityEngine;

namespace Assets.Spripts.Game.ECS.Adapters
{
	public class TransformAdapter : EntityBaseComponent<TransformComponent>
	{
		[SerializeField] private TransformComponent _preset;

		public override void Install(World world, Entity entity)
		{
			base.Install(world, entity);

			ref var component = ref Entity.Get<TransformComponent>();

			component = _preset;
		}
	}
}
