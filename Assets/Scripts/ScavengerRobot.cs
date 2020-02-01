using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerRobot : MonoBehaviour
{
	public GameObject pickupPoint;

	private const float Speed = 10.0f;

	private Controller controller;

	private Part part;

	private List<Part> pickableParts;

    // Start is called before the first frame update
    void Start()
    {
		pickableParts = new List<Part>();

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
			Part bestPart = null;
			float bestDistance = float.PositiveInfinity;

			foreach(Part part in pickableParts)
			{
				Vector3 point = part.GetComponentInChildren<Collider>().ClosestPoint(transform.position);
				float distance = (transform.position - point).magnitude;

				if(distance < bestDistance)
				{
					bestPart = part;
					bestDistance = distance;
				}
			}

			if (part != null)
			{
				part.transform.parent.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
				part = null;
			}

			if (bestPart != null)
			{
				part = bestPart;
				part.transform.parent.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
			}
		}

		if (part != null)
		{
			part.transform.parent.gameObject.transform.position = pickupPoint.transform.position;
			part.transform.parent.gameObject.transform.rotation = pickupPoint.transform.rotation;
		}
			
	}

	public void SetController(Controller controller)
	{
		this.controller = controller;
	}

	private void OnTriggerEnter(Collider other)
	{
		Part part = other.GetComponentInParent<Part>();

		Debug.Log(part);

		if (part != null)
			pickableParts.Add(part);
	}
	private void OnTriggerExit(Collider other)
	{
		Debug.Log(other);
		Part part = other.GetComponentInParent<Part>();

		if (part != null)
			pickableParts.Remove(part);
	}
}
