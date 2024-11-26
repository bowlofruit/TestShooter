using UnityEngine;

public class InputService : IInputService
{
	private InputSystem_Actions _playerInputActions;

	public Vector2 MoveInput => _playerInputActions.Player.Move.ReadValue<Vector2>();
	public Vector2 LookInput => _playerInputActions.Player.Look.ReadValue<Vector2>();

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
