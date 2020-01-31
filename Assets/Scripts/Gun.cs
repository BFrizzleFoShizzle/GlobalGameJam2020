using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
	public GameObject projectileInstance;
	// point in space new projectiles are created at
	public Transform muzzle;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Assert(projectileInstance != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	override public void Attack()
	{
		GameObject projObj = Instantiate(projectileInstance);
		projectileInstance.transform.position = muzzle.position;
		projectileInstance.transform.rotation = robot.transform.rotation;

		Projectile projectile = projectileInstance.GetComponent<Projectile>();
		Debug.Assert(projectile != null);
		projectile.FireFrom(robot);
	}
}
