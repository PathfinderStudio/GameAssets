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
                player.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                player.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                player.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                player.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
