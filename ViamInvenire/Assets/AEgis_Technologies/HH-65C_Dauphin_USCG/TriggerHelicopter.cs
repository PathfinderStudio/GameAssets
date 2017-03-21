using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHelicopter : MonoBehaviour {

    public GameObject Helicopter;
	// Use this for initialization
	void Start ()
    {
        Helicopter.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivateRescueHelicopter()
    {
        Debug.Log("ActivateRescueHelicopter");
        Helicopter.SetActive(true);
    }

}
