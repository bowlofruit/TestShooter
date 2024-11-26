using DefaultEcs.System;
using System;
using UnityEngine;
using Zenject;

public class SystemFixedUpdater : IFixedTickable, IDisposable
{
	private ISystem<float> _system;

	public SystemFixedUpdater(ISystem<float> system)
	{
		_system = system;
	}

	public void Dispose()
	{
		_system.Dispose();
	}

	public void FixedTick()
	{
		_system.Update(Time.fixedDeltaTime);
	}
}
