using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
	public GameObject thisMenu;
	public GameObject lastMenu;
	// Start is called before the first frame update
	void Start()
    {
		Debug.Assert(thisMenu != null);
		Debug.Assert(lastMenu != null);
		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => { Debug.Log("Click"); thisMenu.SetActive(false); lastMenu.SetActive(true); });

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
}
