using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Part
{
	const float Health = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	public override void AddToRobot(Robot r)
	{
		base.AddToRobot(r);

		r.AddHealth(Health);
	}
}
