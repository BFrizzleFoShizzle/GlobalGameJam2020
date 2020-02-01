using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (Input.GetKey(KeyCode.W))
			movement += new Vector3(0, 0, 1);
		else if (Input.GetKey(KeyCode.S))
			movement += new Vector3(0, 0, -1);

		if (Input.GetKey(KeyCode.A))
			movement += new Vector3(-1, 0, 0);
		else if (Input.GetKey(KeyCode.D))
			movement += new Vector3(1, 0, 0);

		if (movement.magnitude > 0.0f)
			movement = movement.normalized;

		return movement;
	}

	public bool IsAttacking()
	{
		return Input.GetKey(KeyCode.Space);
	}
}
