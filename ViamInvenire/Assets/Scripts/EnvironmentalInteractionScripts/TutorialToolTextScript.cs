using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToolTextScript : MonoBehaviour
{
    private GameObject player;
    private static int count = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);
        if (count == 0)
        {
            this.transform.LookAt(player.transform, Vector3.up);
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player" && count == 0)
            {
                this.GetComponent<MeshRenderer>().enabled = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player" && count == 0)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void UpdateTutorialText()
    {
        count++;
    }
}
