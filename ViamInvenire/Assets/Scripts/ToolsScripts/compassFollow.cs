using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class compassFollow : MonoBehaviour
{
    public GameObject buddy;

    private GameObject northLocation;
    private bool playerHolding = false;
    private Vector3 thingToRotateTowards;
    private Vector3 playerFacingVector;
    private float angleBetween;
    private float offset;
    private int sign = -1;


    // Use this for initialization
    void Start()
    {
        northLocation = GameObject.FindGameObjectWithTag("TrueNorth");
        thingToRotateTowards = northLocation.transform.position - this.transform.position;
        //thingToRotateTowards.Normalize();
        playerFacingVector = buddy.transform.forward;
        angleBetween = Vector3.Angle(thingToRotateTowards, playerFacingVector);
        offset = 0;
        //playerHolding = false;
    }


    // Update is called once per frame
    void Update()
    {
        thingToRotateTowards = northLocation.transform.position - this.transform.position;
        thingToRotateTowards.Normalize();
        playerFacingVector = buddy.transform.forward;
        angleBetween = Quaternion.FromToRotation(thingToRotateTowards, playerFacingVector).eulerAngles.y;
        
        Debug.Log("rotateTowards " + thingToRotateTowards);
        Debug.Log("PlayerVector " + playerFacingVector);
        angleBetween = 360f - angleBetween;    
        
        if(buddy.transform.eulerAngles.y > 120.0f && buddy.transform.eulerAngles.y < 150.0f) //if(playerFacingVector.x < 0.9f && playerFacingVector.x > 0.6f && playerFacingVector.z < -0.5f && playerFacingVector.z > -0.8f)
        {
            
            if(buddy.transform.eulerAngles.y > 135.0f)
            {
                //offset = 150.0f - buddy.transform.eulerAngles.y;
                offset = buddy.transform.eulerAngles.y - 120.0f;
                offset *= offset / 4;
            }
            else
            {
                offset = buddy.transform.eulerAngles.y - 120.0f;
                offset *= offset / 5;
            }
            
            angleBetween = angleBetween - offset;
        }
        Debug.Log(angleBetween);
        //thingToRotateTowards.x = this.transform.position.x;
        //this.transform.rotation = Quaternion.LookRotation(thingToRotateTowards, Vector3.forward);
        this.transform.localRotation = Quaternion.Euler(new Vector3(0f, angleBetween + 90f, 0f));
        //this.transform.localRotation = Quaternion.Euler(new Vector3(0f, -camera.transform.rotation.eulerAngles.y + 90, 0f)); // rotates it too point north
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
