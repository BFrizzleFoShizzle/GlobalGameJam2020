using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	const float speed = 10.0f;
	const float damage = 15.0f;
	// robot we came from, ignore collisions with self
	public Robot robot;
    // Start is called before the first frame update
    void Start()
    {
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.velocity = transform.forward * speed;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void FireFrom(Robot r)
	{
		robot = r;
	}

	private void OnTriggerEnter(Collider other)
	{
		Robot otherRobot = other.GetComponentInParent<Robot>();
		if (otherRobot != null && otherRobot != robot)
		{
			// hit player, do damage + destroy projectile?
			otherRobot.TakeDamage(damage);
			Destroy(gameObject);
		}
		else if(other.GetComponent<Wall>() != null)
		{
			// hit wall, destroy projectile
			Destroy(gameObject);
		}
	}
}
