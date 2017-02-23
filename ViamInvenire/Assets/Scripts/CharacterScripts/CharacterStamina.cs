﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStamina : MonoBehaviour
{
    /// <summary>
    /// How long the player can run before running out of stamina.
    /// Larger numbers mean longer run time.
    /// </summary>
    public float runTime = 7.0f;
    public float recoveryRate = 1f;
    public float burnRate = 1f;

    private float currentTimeRunning;
    private bool tired;
    private bool running;
    private float minimumThreshold;


    // Use this for initialization
    void Start()
    {
        currentTimeRunning = 0;
        tired = false;
        running = false;
        minimumThreshold = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if the player is running
        if(!tired && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            running = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            running = false;
        }
        if(tired)
        {
            running = false;
        }
        //If the player is running bring down his stamina
        if(Input.GetKeyDown(KeyCode.Space) && running)
        {
            currentTimeRunning += burnRate * Time.deltaTime * 10; //this is a test to make jumping burn more stamina
        }
        else if(running)
        {
            currentTimeRunning += burnRate * Time.deltaTime;
        }
        else
        {
            currentTimeRunning -= recoveryRate * Time.deltaTime;
            
            if(currentTimeRunning < 0.0f)
            {
                currentTimeRunning = 0.0f;
            }
        }

        if(!tired && currentTimeRunning >= runTime)
        {
            tired = true;
            SendMessage("SetCanRun", false, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            if(!running && currentTimeRunning <= minimumThreshold)
            {
                tired = false;
                SendMessage("SetCanRun", true, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    /// <summary>
    /// Hit the ground from too high up and caused the player to lose their stamina
    /// </summary>
    public void LostStamina()
    {
        currentTimeRunning = runTime;
        tired = true;
        SendMessage("SetCanRun", false, SendMessageOptions.DontRequireReceiver);
    }
}
