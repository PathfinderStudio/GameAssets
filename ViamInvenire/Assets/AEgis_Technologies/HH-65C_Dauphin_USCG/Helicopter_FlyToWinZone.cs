using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Helicopter_FlyToWinZone : MonoBehaviour {

    public float Speed = 100f;
    public GameObject WinZoneContainer;

    private Vector3 currentPos;
    private Vector3 winPos;

    //deltaX and deltaY refer to WORLD SPACE X and Y of the triangle which we perform the arctan on
    private float deltaX;
    private float deltaZ;
    private float theta;
    private float targetTheta;
    
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
        getTargetAngle();
        if (transform.eulerAngles.y < targetTheta)
        {
            transform.eulerAngles += new Vector3(0f, 30f * Time.deltaTime, 0f);
        }
        if (transform.eulerAngles.y > targetTheta)
        {
            transform.eulerAngles -= new Vector3(0f, 30f * Time.deltaTime, 0f);
        }
        transform.position += -1f*(transform.forward * Time.deltaTime * Speed);
    }
}
