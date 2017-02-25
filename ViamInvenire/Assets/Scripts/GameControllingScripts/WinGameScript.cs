using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameScript : MonoBehaviour {

    private GameObject player;
    private int flareGunIndex;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for(int i = 0; i < player.transform.childCount; i++)
        {
            Transform currentChild = player.transform.GetChild(i);
            for(int j = 0; j < currentChild.childCount; j++)
            {
                if(currentChild.GetChild(j).gameObject.tag == "Flaregun")
                {
                    flareGunIndex = j;
                }
            }
        }
        player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player")
            {
                player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.SetActive(true);
                for(int i = 0; i < player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.childCount; i++)
                {
                    player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
                }

                //player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                //player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                //player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                //player.transform.GetChild(4).transform.GetChild(flareGunIndex).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
