using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{

    private bool isRaised = false;
    private bool isHolding = true;
    private bool mapCanBeUsed = true;
    private int mapState = 0;
    private Vector3 lowLocation;
    private Vector3 highLocation;
    private Vector3 lowRotation;
    private Vector3 highRotation;
    private float lowScale;
    private float highScale;

    // Use this for initialization
    void Start()
    {
        lowLocation = new Vector3(-0.341f, -0.250f, 1.026f);
        highLocation = new Vector3(0.009f, 0.0f, 0.836f);
        lowRotation = new Vector3(1.35f, 76f, 13.48f);
        highRotation = new Vector3(-1.25f, 91.22f, 3.35f);
        lowScale = 0.77f;
        highScale = 1.89f; //1.54f
        this.transform.localPosition = lowLocation;
        this.transform.localEulerAngles = lowRotation;
        this.transform.localScale = new Vector3(lowScale, lowScale, lowScale);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (mapCanBeUsed)
        {
            if (isRaised)
            {

                if (Input.GetKeyUp(KeyCode.Mouse0) && Input.GetKeyUp(KeyCode.Mouse1))
                {
                    //Something special to do here?
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    isRaised = false;
                    lowerMap();
                    //Put back down
                }
                else if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    //zoom in
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.Mouse0) && Input.GetKeyUp(KeyCode.Mouse1))
                {
                    //unknown action
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    isRaised = true;
                    raiseMap();
                    //bring up
                }
                else if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    //do something when lowered
                }
            }
        }
        */
    }

    public void SetMapState(int state)
    {
        mapState = state;
        if(mapCanBeUsed)
        {
            if(mapState == 0)
            {
                isRaised = false;
                lowerMap();
            }
            else if(mapState == 1)
            {
                //map will be shown
            }
            else if(mapState == 2)
            {
                isRaised = true;
                raiseMap();
            }
        }

    }

    private void lowerMap()
    {
        this.transform.localPosition = lowLocation;
        this.transform.localEulerAngles = lowRotation;
        this.transform.localScale = new Vector3(lowScale, lowScale, lowScale);
    }

    private void raiseMap()
    {
        this.transform.localPosition = highLocation;
        this.transform.localEulerAngles = highRotation;
        this.transform.localScale = new Vector3(highScale, highScale, highScale);
    }

    public void itemPickedUp(bool input)
    {
        isHolding = input;
    }

    public bool isPlayerHolding()
    {
        return isHolding;
    }

    /// <summary>
    /// If binoculars are in use, map cannot be used.
    /// </summary>
    /// <param name="input"></param>
    public void viewBusy(bool input)
    {
        mapCanBeUsed = !input;
        if(input)
        {
            lowerMap();
        }
    }
}
