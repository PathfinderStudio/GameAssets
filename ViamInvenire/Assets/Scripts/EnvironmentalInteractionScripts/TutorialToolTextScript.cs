using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
       // Debug.Log(count);
        if (count == 0)
        {
            this.transform.LookAt(player.transform, Vector3.up);
            this.GetComponent<TextMeshPro>().text = "Press E on Items to pick them up.";

        }
        else if(count == 1)
        {
            this.transform.LookAt(player.transform, Vector3.up);
            this.GetComponent<TextMeshPro>().text = "The rest of the tools will be littered around the map for you to find.";
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
            if (other.tag == "Player" && count < 2)
            {
                this.GetComponent<MeshRenderer>().enabled = true;

            }
            else if(count > 1)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Time.timeScale != 0)
        {
            if (other.tag == "Player" && count < 2)
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
