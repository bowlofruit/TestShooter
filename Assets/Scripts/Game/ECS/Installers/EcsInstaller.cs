using DefaultEcs;
using Zenject;

public class EcsInstaller : MonoInstaller
{
	private World _world;

	public override void InstallBindings()
	{
		_world = new World();
		Container.Bind<World>().FromInstance(_world).AsSingle();
	}
}