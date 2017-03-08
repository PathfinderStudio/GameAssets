using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helicopter_FlyToWinZone : MonoBehaviour {

    public float Speed = 100f;
    public float TurnSpeed = 30f;
    public GameObject WinZoneContainer;

    private float terrainHeight;

    private GameObject winZone;

    private Vector3 currentPos;
    private Vector3 winPos;
    private bool atWinZone = false;

    //deltaX and deltaY refer to WORLD SPACE X and Y of the triangle which we perform the arctan on
    private float deltaX;
    private float deltaZ;
    private float theta;
    private float targetTheta;

    private float circleStartAngle;
    private float circleAngleTraversed;
    private float speedDecreaseFactor;
    private float descentRate = 20.0f;
    
	// Use this for initialization
	void Start()
    {
        //Find where the helicopter is placed in the map
        currentPos = transform.position;

        //Find position of active winZone
		for(int i =0; i < WinZoneContainer.transform.childCount; i++)
		{
			if(WinZoneContainer.transform.GetChild(i).gameObject.activeSelf)
            {
                winZone = WinZoneContainer.transform.GetChild(i).gameObject;
                winPos = WinZoneContainer.transform.GetChild(i).position;
                Debug.Log(winPos.x + "," + winPos.y + "," + winPos.z);
            }
		}

        
        deltaX = winPos.x - currentPos.x;
        deltaZ = winPos.z - currentPos.z;
        targetTheta = Mathf.Atan2(deltaX, deltaZ) * (180.0f/3.14159f);

        if(targetTheta < 0f)
        {
            targetTheta += 180f;
        }
        Debug.Log("Theta: " + theta);
        
    }
	void getTargetAngle()
    {
        deltaX = winPos.x - currentPos.x;
        deltaZ = winPos.z - currentPos.z;
        targetTheta = Mathf.Atan2(deltaX, deltaZ) * (180.0f / 3.14159f);

        if (targetTheta < 0f)
        {
            targetTheta += 180f;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        /*
        getTargetAngle();
        if (transform.eulerAngles.y < targetTheta)
        {
            transform.eulerAngles += new Vector3(0f, 30f * Time.deltaTime, 0f);
        }
        if (transform.eulerAngles.y > targetTheta)
        {
            transform.eulerAngles -= new Vector3(0f, 30f * Time.deltaTime, 0f);
        }*/
        if(!atWinZone)
        {
            transform.LookAt(winZone.transform);
            transform.eulerAngles = new Vector3(0.0f, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
            transform.position += (transform.forward * Time.deltaTime * Speed);

            if((transform.position.x > winZone.transform.position.x - 20.0f) && (transform.position.x < winZone.transform.position.x + 20.0f) &&    //if Helicopter's x and z pos are within 40 units of winZone x and z pos
                (transform.position.z > winZone.transform.position.z - 20.0f) && (transform.position.z < winZone.transform.position.z + 20.0f))
            {
                atWinZone = true;
                circleStartAngle = transform.eulerAngles.y;
                circleAngleTraversed = 0.0f;
                speedDecreaseFactor = Speed / 360f;
            }
        }
        else if(atWinZone)
        {
            terrainHeight = Terrain.activeTerrain.SampleHeight(transform.position);
            transform.eulerAngles += new Vector3(0f, TurnSpeed * Time.deltaTime, 0f);
            circleAngleTraversed += TurnSpeed * Time.deltaTime;

            if(circleAngleTraversed > 320.0f)
            {
                Speed -= 15.0f * Time.deltaTime;
                Speed = Mathf.Clamp(Speed, 0.0f, float.PositiveInfinity);
            }
            if(circleAngleTraversed > 360.0f)
            {
                //TODO: Make helicopter jostle based on an oscillating sin func
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.position.y,Mathf.Sin()
                TurnSpeed = 0.0f;
                if(transform.position.y > terrainHeight + 5.0f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - (descentRate * Time.deltaTime), transform.position.z);
                    if(transform.position.y - terrainHeight < 50.0f)
                    {
                        descentRate -= 10.0f * Time.deltaTime;
                    }
                    descentRate = Mathf.Clamp(descentRate, 5.0f, float.PositiveInfinity);
                    if (transform.position.y - terrainHeight < 25.0f)
                    {
                        descentRate -= 10.0f * Time.deltaTime;
                        descentRate = Mathf.Clamp(descentRate, 0.1f, float.PositiveInfinity);
                    }
                    
                }
            }
            transform.position += (transform.forward * Time.deltaTime * Speed);
        }
    }
}
