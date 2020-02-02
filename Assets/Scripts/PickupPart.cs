using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPart : MonoBehaviour
{
	public Light pointLight;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Assert(pointLight != null);
    }

    // Update is called once per frame
    void Update()
    {
		pointLight.transform.position = transform.position + new Vector3(0, 1, 0);
		if (GetComponentInChildren<Gun>() != null)
			pointLight.color = new Color(1, 0, 0);
		if (GetComponentInChildren<Legs>() != null)
			pointLight.color = new Color(0, 1, 0);
		if (GetComponentInChildren<Head>() != null)
			pointLight.color = new Color(0, 0, 1);
		//pointLight.transform.rotation = Quaternion.Euler(90, 0, 0);
	}
}
