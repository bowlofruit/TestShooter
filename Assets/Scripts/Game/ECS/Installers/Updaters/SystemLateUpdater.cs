using DefaultEcs.System;
using System;
using UnityEngine;
using Zenject;

public class SystemLateUpdater : ILateTickable, IDisposable
{
	private ISystem<float> _system;

	public SystemLateUpdater(ISystem<float> system)
	{
		_system = system;
	}

	public void Dispose()
	{
		_system.Dispose();
	}

	public void LateTick()
	{
		_system.Update(Time.deltaTime);
	}
}
