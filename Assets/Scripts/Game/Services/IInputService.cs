using UnityEngine;

public interface IInputService
{
	public Vector2 MoveInput { get; }
	public int ShootInput { get; }
}