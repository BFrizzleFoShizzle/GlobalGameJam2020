﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => { Application.Quit(); });
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
