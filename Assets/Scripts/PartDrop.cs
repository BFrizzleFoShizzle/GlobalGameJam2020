using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDrop : MonoBehaviour
{
	public Material regularMaterial;
	public Material playerInMaterial;
    // Start is called before the first frame update
    void Start()
    {
		Debug.Log(regularMaterial != null);
		Debug.Log(playerInMaterial != null);
	}

    // Update is called once per frame
    void Update()
    {

    }

	public void PlayerEnter()
	{
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.material = playerInMaterial;
	}
	public void PlayerExit()
	{
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.material = regularMaterial;
	}
}
