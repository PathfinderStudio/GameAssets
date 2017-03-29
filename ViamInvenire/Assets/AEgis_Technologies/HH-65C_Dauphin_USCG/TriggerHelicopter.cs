using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHelicopter : MonoBehaviour {

    public GameObject Helicopter;

    private GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Helicopter.SetActive(false);
	}
	
    public void ActivateRescueHelicopter()
    {
        //Debug.Log("ActivateRescueHelicopter");
        Helicopter.SetActive(true);
        player.SendMessage("GameWon", SendMessageOptions.DontRequireReceiver);
    }

}
