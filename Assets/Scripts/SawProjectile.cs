﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		// destroy after one frame
		Destroy(gameObject);
    }
}
