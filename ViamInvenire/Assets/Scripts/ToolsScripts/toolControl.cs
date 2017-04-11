using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolControl : MonoBehaviour
{

    private bool canSwitch;
    private int compassIndex;
    private int binocularsIndex;
    private int flaregunIndex;
    private int flashlightIndex;
    private int emergencyFlareIndex;
    private int mapIndex;
    private MapState mapState;
    private bool[] selected;
    public GameObject InventoryUI;

    private enum Tools
    {
        EmergencyFlare,
        Compass,
        Flashlight,
        Binoculars,
        Flaregun
    }

    private enum MapState
    {
        PutAway,
        LowPosition,
        RaisedPosition
    }

    // Use this for initialization
    void Start()
    {
        canSwitch = true;
        selected = new bool[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.tag == "Binoculars")
            {
                binocularsIndex = i;
                selected[(int)Tools.Binoculars] = false;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flaregun")
            {
                flaregunIndex = i;
                selected[(int)Tools.Flaregun] = false;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flashlight")
            {
                flashlightIndex = i;
                selected[(int)Tools.Flashlight] = false;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Compass")
            {
                compassIndex = i;
                selected[(int)Tools.Compass] = false;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flare")
            {
                emergencyFlareIndex = i;
                selected[(int)Tools.EmergencyFlare] = false;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Map")
            {
                mapIndex = i;
            }


        }
        mapState = MapState.PutAway;
        this.transform.GetChild(mapIndex).SendMessage("SetMapState", (int)mapState, SendMessageOptions.DontRequireReceiver);
        switchItems(false, false, false, false, false, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwitch)
        {
            if(mapState != MapState.RaisedPosition)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0)) // to select no item
                {
                    switchItems(false, false, false, false, false, false);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1)) // to select flares
                {
                    switchItems(false, false, false, false, false, true);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2)) // to select compass
                {
                    switchItems(true, false, false, false, false, false);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3)) // to select flashlight
                {
                    switchItems(false, true, true, false, false, false);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4)) // to select binoculars
                {
                    switchItems(false, false, false, true, false, false);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5)) // to select flare gun
                {
                    switchItems(false, false, false, false, true, false);
                }
                else if (Input.GetKeyDown(KeyCode.Tab)) //to show inventory
                {
                    InventoryUI.GetComponent<InventoryUI>().showInventory();
                }
            }
            if(Input.GetKeyUp(KeyCode.M))
            {
                if(mapState == MapState.PutAway)
                {
                    mapState = MapState.LowPosition;
                    this.transform.GetChild(mapIndex).gameObject.SendMessage("SetMapState", (int)mapState, SendMessageOptions.DontRequireReceiver);
                    this.transform.GetChild(mapIndex).gameObject.SetActive(true);
                }
                else if(mapState == MapState.LowPosition)
                {
                    mapState = MapState.RaisedPosition;
                    this.transform.GetChild(mapIndex).gameObject.SendMessage("SetMapState", (int)mapState, SendMessageOptions.DontRequireReceiver);
                    //remember last item that was out before making map big
                    switchItems(false, false, false, false, false, false);
                }
                else if(mapState == MapState.RaisedPosition)
                {
                    mapState = MapState.PutAway;
                    this.transform.GetChild(mapIndex).gameObject.SendMessage("SetMapState", (int)mapState, SendMessageOptions.DontRequireReceiver);
                    this.transform.GetChild(mapIndex).gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void switchEnabled(bool value)
    {
        canSwitch = value;
    }

    /// <summary>
    /// Determines which item to switch to and sets other inactive.
    /// </summary>
    /// <param name="compassActive"></param>
    /// <param name="flashlightActice"></param>
    /// <param name="flashLightOn"></param>
    /// <param name="binocularsActive"></param>
    /// <param name="flareGunActive"></param>
    /// <param name="emergencyFlareSelected"></param>
    /// <param name="mapSelected"></param>
    public void switchItems(bool compassActive, bool flashlightActice, bool flashLightOn, bool binocularsActive, bool flareGunActive, bool emergencyFlareSelected)
    {
        //sets compass
        if (this.transform.GetChild(compassIndex).transform.GetChild(3).GetComponent<compassFollow>().isPlayerHolding())
        {
            this.transform.GetChild(compassIndex).gameObject.SetActive(compassActive);
            //this.transform.GetChild(compassIndex).transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer>().enabled = compassActive;
            //this.transform.GetChild(compassIndex).GetComponentInChildren<MeshRenderer>().enabled = compassActive;
        }

        //sets flashlight
        if (this.transform.GetChild(flashlightIndex).GetComponent<flashLightControl>().isPlayerHolding())
        {
            this.transform.GetChild(flashlightIndex).GetComponent<MeshRenderer>().enabled = flashlightActice;
            this.transform.GetChild(flashlightIndex).GetChild(0).GetComponent<Light>().enabled = flashLightOn;
        }

        //sets binoculars
        if (this.transform.GetChild(binocularsIndex).GetComponent<binocularControl>().isPlayerHolding())
        {
            this.transform.GetChild(binocularsIndex).gameObject.SetActive(binocularsActive);
            if(this.transform.GetChild(mapIndex).gameObject.activeInHierarchy && this.transform.GetChild(binocularsIndex).gameObject.GetComponent<binocularControl>().binocularCamera.enabled)
            {
                this.transform.GetChild(mapIndex).gameObject.SendMessage("viewBusy", binocularsActive, SendMessageOptions.DontRequireReceiver); //if the binoculars are in use don't allow map to be visible
            }
        }

        //sets flaregun
        if(this.transform.GetChild(flaregunIndex).GetComponent<flaregun>().isPlayerHolding())
        {
            this.transform.GetChild(flaregunIndex).gameObject.SetActive(flareGunActive);
        }
        
        //set emergency flares
        if (this.transform.GetChild(emergencyFlareIndex).GetComponent<emergencyFlareControl>().isPlayerHolding())
        {
            this.transform.GetChild(emergencyFlareIndex).gameObject.SetActive(emergencyFlareSelected);
        }
    }

}