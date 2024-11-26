using UnityEngine;

public interface IInputService
{
	public Vector2 MoveInput { get; }
	public Vector2 LookInput { get; }
}