using UnityEngine;

public class InputService : IInputService
{
	private InputSystem_Actions _playerInputActions;

	public Vector2 MoveInput => _playerInputActions.Player.Move.ReadValue<Vector2>();
	public int ShootInput
	{
		get
		{
			if (_playerInputActions.Player.Shoot.WasPressedThisFrame()) return 1;
			if (_playerInputActions.Player.Shoot.WasPressedThisFrame()) return 2;
			if (_playerInputActions.Player.Shoot.WasPressedThisFrame()) return 3;
			return 0;
		}
	}

	public InputService()
	{
		_playerInputActions = new InputSystem_Actions();
		_playerInputActions.Enable();
	}

	public void Dispose()
	{
		_playerInputActions.Disable();
	}
}
