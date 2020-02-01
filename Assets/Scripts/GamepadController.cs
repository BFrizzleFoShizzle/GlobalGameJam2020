﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadController : Controller
{
	public GamepadController()
	{
		Debug.Log("Gamepad controller created");
	}

	public Vector3 GetFacingDirection()
	{
		return GetMovementDirection().normalized;
	}

	public Vector3 GetMovementDirection()
	{
		Vector2 stick = Gamepad.current.leftStick.ReadValue();

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
		return Gamepad.current.buttonSouth.isPressed;
	}
}
