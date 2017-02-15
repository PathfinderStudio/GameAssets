using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameScript : MonoBehaviour {

    public GameObject winLocation;

	// Use this for initialization
	void Start ()
    {
        this.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WinLocation")
        {
            this.transform.GetChild(4).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
