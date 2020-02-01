using System;
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

	public MountPoint legs;
	public GameObject body;

	private float health;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Assert(legs != null);
		Debug.Assert(body != null);

		health = 100.0f;

		if (legs.part != null)
			SetupMount(legs);

		foreach (MountPoint mount in mountPoints)
		{
			if(mount.part != null)
			{
				SetupMount(mount);
			}
		}
		
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
		rigidbody.velocity = movement * Speed * GetSpeed();

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
		// legs: special case
		if(part is Legs)
		{
			AddLegs(part as Legs);
			part.AddToRobot(this);
			return;
		}

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

	public void AddLegs(Legs legs)
	{
		this.legs.part = legs;
		body.transform.localPosition += new Vector3(0, legs.height, 0);
	}

	public float GetSpeed()
	{
		Legs legsPart = legs.part as Legs;
		if (legsPart == null)
			return 0.0f;
		else
			return legsPart.speed;
	}
	public Legs GetLegs()
	{
		Legs legsPart = legs.part as Legs;
		return legsPart;
	}

	private void SetupMount(MountPoint mount)
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
