using DefaultEcs;
using DefaultEcs.System;
using Zenject;

public class SystemInstaller : MonoInstaller
{
	private World _world;
	private IInputService _inputService;
	private IBulletFactory _bulletFactory;

	private ISystem<float> CreateUpdateSystem => new SequentialSystem<float>
		(
			new WeaponSystem(_world, _bulletFactory, _inputService),
			new BulletLifetimeSystem(_world, _bulletFactory)
		);

	private ISystem<float> CreateFixedUpdateSystem => new SequentialSystem<float>
		(
			new PlayerMovementSystem(_world, _inputService),
			new BulletMovementSystem(_world, _inputService)
		);

	private ISystem<float> CreateLateUpdateSystem => new SequentialSystem<float>();

	[Inject]
	private void Construct(World world, IBulletFactory bulletFactory, IInputService inputService)
	{
		_world = world;
		_inputService = inputService;
		_bulletFactory = bulletFactory;
	}

	public override void InstallBindings()
	{
		Container.BindInterfacesAndSelfTo<SystemUpdater>().FromInstance(new SystemUpdater(CreateUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemFixedUpdater>().FromInstance(new SystemFixedUpdater(CreateFixedUpdateSystem)).AsSingle();
		Container.BindInterfacesAndSelfTo<SystemLateUpdater>().FromInstance(new SystemLateUpdater(CreateLateUpdateSystem)).AsSingle();
	}
}