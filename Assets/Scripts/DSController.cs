using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class DSController : Controller
{
	public DSController()
	{
		Debug.Log("DualShock controller created");
	}

	public Vector3 GetFacingDirection()
	{
		return GetMovementDirection().normalized;
	}

	public Vector3 GetMovementDirection()
	{
		Vector2 stick = DualShockGamepad.current.leftStick.ReadValue();

		Debug.Log(stick);

		// Magnitude is usually not larger than 1, but sometimes is?
		Vector3 movement = new Vector3(stick.x, 0, stick.y);

		if (movement.magnitude > 1.0f)
			movement = movement.normalized;

		Debug.Assert(movement.magnitude <= 1.001f);

		return movement;
	}

	public bool IsAttacking()
	{
		return DualShockGamepad.current.xButton.isPressed;
	}
}
