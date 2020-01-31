using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	// robot we came from, ignore collisions with self
	public Robot robot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void FireFrom(Robot r)
	{
		robot = r;
	}
}
