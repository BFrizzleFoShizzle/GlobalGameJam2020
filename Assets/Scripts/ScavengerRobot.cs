using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerRobot : MonoBehaviour
{
	private const float Speed = 10.0f;

	private Controller controller;

	private Part part;

    // Start is called before the first frame update
    void Start()
    {
        
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

		if (controller.IsDoingPickUp())
		{

		}
	}

	public void SetController(Controller controller)
	{
		this.controller = controller;
	}
}
