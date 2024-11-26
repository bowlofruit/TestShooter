using DefaultEcs.System;
using System;
using UnityEngine;
using Zenject;

public class SystemUpdater : ITickable, IDisposable
{
	private ISystem<float> _system;

	public SystemUpdater (ISystem<float> system)
	{
		_system = system;
	}

	public void Dispose()
	{
		_system.Dispose();
	}

	public void Tick()
	{
		_system.Update(Time.deltaTime);
	}
}
