using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        foreach(MountPoint mount in mountPoints)
		{
			mount.part.AddToRobot(this);
			if (mount.part is Weapon)
				weapons.Add(mount.part as Weapon);
		}

		Debug.Log(Input.GetJoystickNames());

		if(Input.GetJoystickNames().Length > 0)
			controller = new GamepadController();

		if (controller == null)
			controller = new KeyboardController();

	}

    // Update is called once per frame
    void Update()
    {
		Vector3 movement = controller.GetMovementDirection();

		if (movement.magnitude > 0)
		{
			transform.rotation = Quaternion.FromToRotation(new Vector3(0, 0, 1), movement);
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
}
