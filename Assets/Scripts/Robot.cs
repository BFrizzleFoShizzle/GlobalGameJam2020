﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

public class Robot : MonoBehaviour
{
	private const float Speed = 3.0f;

	[Serializable]
	public class MountPoint
	{
		public Transform point;
		public Part part;
	}

	public List<MountPoint> mountPoints;
	private Controller controller;
	// prolly in order of key binding (if each weapon is controlled separately)
	private List<Weapon> weapons = new List<Weapon>();

	private float health;

    // Start is called before the first frame update
    void Start()
    {
		health = 100.0f;

		foreach (MountPoint mount in mountPoints)
		{
			if(mount.part != null)
			{
				mount.part.transform.SetParent(mount.point);
				mount.part.transform.localRotation = Quaternion.identity;
				mount.part.transform.localPosition = Vector3.zero;
				mount.part.transform.localScale = Vector3.one;
				mount.part.AddToRobot(this);
				if (mount.part is Weapon)
					weapons.Add(mount.part as Weapon);
			}
		}
		/*
		foreach (DualShockGamepad ds in DualShockGamepad.all)
		{
			Debug.Log(ds);
			Debug.Log(ds.enabled);
			Debug.Log(ds.lastUpdateTime);
		}

		if (DualShockGamepad.current != null)
			controller = new DSController();
			*/


		//Debug.Log(Gamepad.current.added);
		//Debug.Log(Gamepad.current.added);

		//if (controller == null && XInputController.current != null)
		//	controller = new XboxController();

		if (controller == null && Gamepad.current != null)
			controller = new GamepadController();

		if (controller == null)
			controller = new KeyboardController();

		Debug.Log(controller);
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 movement = controller.GetMovementDirection();

		if (movement.magnitude > 0)
		{
			//transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), movement);
			transform.rotation = Quaternion.LookRotation(movement, new Vector3(0, 1, 0));
		}

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = movement * Speed;

		if (controller.IsAttacking())
		{
			foreach(Weapon weapon in weapons)
			{
				weapon.Attack();
			}
		}
	}

	public void AddPartToRandomPoint(Part part)
	{
		List<MountPoint> unusedMounts = new List<MountPoint>();

		foreach(MountPoint mountPoint in mountPoints)
		{
			if (mountPoint.part == null)
			{
				unusedMounts.Add(mountPoint);
			}
		}

		// add to robot if there's a free mount
		if (unusedMounts.Count > 0)
		{
			int mountIndex = UnityEngine.Random.Range(0, unusedMounts.Count);
			unusedMounts[mountIndex].part = part;
			part.AddToRobot(this);
			// add to weapons list if needed
			if (part is Weapon)
				weapons.Add(part as Weapon);
		}
	}

	public void AddHealth(float health)
	{
		this.health += health;
	}

	public void SetController(Controller controller)
	{
		this.controller = controller;
	}

	public bool IsAlive()
	{
		return health > 0.0f;
	}
}
