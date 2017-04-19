using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compassFollow : MonoBehaviour
{
    public GameObject camera;

    private GameObject northLocation;
    private bool playerHolding = false;
    private Vector3 thingToRotateTowards;

    // Use this for initialization
    void Start()
    {
        northLocation = GameObject.FindGameObjectWithTag("TrueNorth");
        thingToRotateTowards = northLocation.transform.position - this.transform.position;
        thingToRotateTowards.x = this.transform.position.x;
        //playerHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        //thingToRotateTowards = northLocation.transform.position - this.transform.position;
        //thingToRotateTowards.x = this.transform.position.x;
        //this.transform.rotation = Quaternion.LookRotation(thingToRotateTowards, Vector3.forward);
        //this.transform.localRotation = Quaternion.Euler(new Vector3(0f, yAngle, 0f));
        this.transform.localRotation = Quaternion.Euler(new Vector3(0f, -camera.transform.rotation.eulerAngles.y + 90, 0f)); // rotates it too point north
    }

    public void itemPickedUp(bool input)
    {
        playerHolding = input;
    }

    public bool isPlayerHolding()
    {
        return playerHolding;
    }
}
