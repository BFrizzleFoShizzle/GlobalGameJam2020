using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
	// robot this part is from
	protected Robot robot;
    // Start is called before the first frame update
    void Start()
    {
		//Debug.Assert(robot != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	virtual public void AddToRobot(Robot r)
	{
		robot = r;
	}
}
