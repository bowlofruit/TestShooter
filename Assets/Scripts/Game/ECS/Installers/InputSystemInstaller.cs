using Zenject;

public class InputSystemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		Container.Bind<IInputService>().To<InputService>().AsSingle();
	}
}