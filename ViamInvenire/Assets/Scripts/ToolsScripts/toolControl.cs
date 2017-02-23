using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolControl : MonoBehaviour {

	private bool canSwitch;
    private int compassIndex;
    private int binocularsIndex;
    private int flaregunIndex;
    private int flashlightIndex;

    // Use this for initialization
    void Start () 
	{
		canSwitch = true;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.tag == "Binoculars")
            {
                binocularsIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flaregun")
            {
                flaregunIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flashlight")
            {
                flashlightIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Compass")
            {
                compassIndex = i;
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (canSwitch)
		{
			if (Input.GetKeyDown (KeyCode.Alpha0)) // to select no item
			{
                switchItems(false, false, false, false, false);
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha1)) // to select compass
			{
                switchItems(true, false, false, false, false);
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha2)) // to select flashlight
			{
                switchItems(false, true, true, false, false);
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha3)) // to select binoculars
			{
                switchItems(false, false, false, true, false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) // to select flare gun
            {
                switchItems(false, false, false, false, true);
            }
        }
	}

	private void switchEnabled(bool value)
	{
		canSwitch = value;
	}

    private void switchItems(bool compassActive, bool flashlightActice, bool flashLightOn, bool binocularsActive, bool flareGunActive)
    {
        //sets compass
        if (this.transform.GetChild(compassIndex).transform.GetChild(3).GetComponent<compassFollow>().isPlayerHolding())
        {
            this.transform.GetChild(compassIndex).GetComponentInChildren<MeshRenderer>().enabled = compassActive;
            this.transform.GetChild(compassIndex).transform.GetChild(3).transform.GetChild(2).GetComponentInChildren<MeshRenderer>().enabled = compassActive;
        }

        Debug.Log(this.transform.GetChild(flashlightIndex).GetComponent<flashLightControl>().isPlayerHolding());
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
        }

        //sets flaregun
        for (int i = 0; i < this.transform.GetChild(flaregunIndex).childCount - 1; i++) // -1 for barellend not having a meshrender
        {
            this.transform.GetChild(flaregunIndex).transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = flareGunActive;
        }
    }

}
