﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight_lookAtPlayer : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
	}
}
