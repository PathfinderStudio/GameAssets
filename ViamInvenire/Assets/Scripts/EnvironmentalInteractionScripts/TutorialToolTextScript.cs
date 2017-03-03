using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToolTextScript : MonoBehaviour
{
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform, Vector3.up);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if(other.tag == "Player")
            {
                this.GetComponent<MeshRenderer>().enabled = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player")
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
