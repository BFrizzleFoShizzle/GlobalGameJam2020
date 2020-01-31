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
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 movement = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
			movement += new Vector3(0, 0, 1);
		else if (Input.GetKey(KeyCode.S))
			movement += new Vector3(0, 0, -1);

		if (Input.GetKey(KeyCode.A))
			movement += new Vector3(-1, 0, 0);
		else if (Input.GetKey(KeyCode.D))
			movement += new Vector3(1, 0, 0);

		if(Input.GetKey(KeyCode.Space))
		{
			foreach(Weapon weapon in weapons)
			{
				weapon.Attack();
			}
		}

		Rigidbody rigidbody = GetComponent<Rigidbody>();

		rigidbody.velocity = movement.normalized * Speed;
		rigidbody.rotation = Quaternion.FromToRotation(new Vector3(0,0,1), movement);
	}
}
