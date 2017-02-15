using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameScript : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player")
            {
                player.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
