using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadController : Controller
{
	public Vector3 GetFacingDirection()
	{
		return GetMovementDirection().normalized;
	}

	public Vector3 GetMovementDirection()
	{
		// Magnitude is usually not larger than 1, but sometimes is?
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (movement.magnitude > 1.0f)
			movement = movement.normalized;

		Debug.Assert(movement.magnitude <= 1.001f);

		return movement;
	}

	public bool IsAttacking()
	{
		return Input.GetButton("Fire1");
	}
}
