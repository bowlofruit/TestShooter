using DefaultEcs;
using UnityEngine;
using Zenject;

public class EntityBase : MonoBehaviour
{
	public ABaseAdapter[] Adapters;

	public Entity Entity { get; private set; }

	public Entity CreateEntity(World world)
	{
		var entity = world.CreateEntity();
		Entity = entity;

		if (Adapters != null)
		{
			foreach (var adapter in Adapters)
			{
				adapter.Install(world, entity);
			}
		}

		return entity;
	}
}
