using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Controller
{
	Vector3 GetMovementDirection();

	// Twin stick? might be needed if we can't rotate weapons/etc
	Vector3 GetFacingDirection();

	// TODO multiple weapons?
	bool IsAttacking();
}
