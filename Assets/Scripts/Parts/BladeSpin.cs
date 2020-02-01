using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour
{
	const float Speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(new Vector3(Speed, 0, 0));
    }
}
