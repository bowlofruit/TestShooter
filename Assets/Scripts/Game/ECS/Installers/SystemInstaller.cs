using DefaultEcs;
using DefaultEcs.System;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	private World _world;
	private IInputService _inputService;

	private ISystem<float> _updateSystem;
	private ISystem<float> _fixedUpdateSystem;
	private ISystem<float> _lateUpdateSystem;

	[Inject]
	private void Construct(World world)
	{
		_world = world;
		_inputService = new InputService();
	}

	public override void InstallBindings()
	{
		_updateSystem = new SequentialSystem<float>
		(
			new PlayerMovementSystem(_world, _inputService),
			new BulletMovementSystem(_world, _inputService)
		);
		_fixedUpdateSystem = new SequentialSystem<float>();
		_lateUpdateSystem = new SequentialSystem<float>();

		SystemUpdater systemUpdater = new SystemUpdater(_updateSystem);
		SystemFixedUpdater systemFixedUpdater = new SystemFixedUpdater(_fixedUpdateSystem);
		SystemLateUpdater systemLateUpdater = new SystemLateUpdater(_lateUpdateSystem);

		Container.BindInterfacesAndSelfTo<SystemUpdater>().FromInstance(systemUpdater).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>().FromInstance(systemFixedUpdater).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemLateUpdater>().FromInstance(systemLateUpdater).AsSingle();
	}
}