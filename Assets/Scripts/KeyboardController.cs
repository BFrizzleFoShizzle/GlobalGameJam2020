using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardController : Controller
{
	public Vector3 GetFacingDirection()
	{
		return GetMovementDirection();
	}

	public Vector3 GetMovementDirection()
	{
		Vector3 movement = new Vector3(0, 0, 0);

		
		// Keyboard controls
		if (Keyboard.current.wKey.isPressed)
			movement += new Vector3(0, 0, 1);
		else if (Keyboard.current.sKey.isPressed)
			movement += new Vector3(0, 0, -1);

		if (Keyboard.current.aKey.isPressed)
			movement += new Vector3(-1, 0, 0);
		else if (Keyboard.current.dKey.isPressed)
			movement += new Vector3(1, 0, 0);

		if (movement.magnitude > 0.0f)
			movement = movement.normalized;
			
		return movement;
	}

	public bool IsAttacking()
	{
		return Keyboard.current.spaceKey.isPressed;
	}

	public bool IsDoingPickUp()
	{
		return Keyboard.current.spaceKey.wasPressedThisFrame;
	}
}
