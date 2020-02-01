using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
	public float MaxReloadTime = 0.5f;
	public GameObject projectileInstance;
	// point in space new projectiles are created at
	public Transform muzzle;

	private float reloadTime;

	// Start is called before the first frame update
	void Start()
    {
		Debug.Assert(projectileInstance != null);
    }

    // Update is called once per frame
    void Update()
    {
        if(reloadTime > 0.0f)
		{
			reloadTime -= Time.deltaTime;
		}
    }

	override public void Attack()
	{
		if (CanFire())
		{
			GameObject projObj = Instantiate(projectileInstance);
			projObj.transform.position = muzzle.position;
			projObj.transform.rotation = robot.transform.rotation;

			Projectile projectile = projObj.GetComponent<Projectile>();
			Debug.Assert(projectile != null);
			projectile.FireFrom(robot);

			reloadTime = MaxReloadTime;
		}
	}

	private bool CanFire()
	{
		return reloadTime <= 0.0f;
	}
}
