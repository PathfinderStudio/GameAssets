using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControl : MonoBehaviour {

	public GameObject flashLight;
	public GameObject compass;
	public GameObject binoculars;

	private bool canSwitch;

	// Use this for initialization
	void Start () 
	{
		canSwitch = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canSwitch)
		{
			if (Input.GetKeyDown (KeyCode.Alpha0)) // to select no item
			{
				compass.GetComponentInChildren<MeshRenderer> ().enabled = false;
				compass.transform.GetChild (3).GetComponentInChildren<MeshRenderer> ().enabled = false;
				flashLight.GetComponent<MeshRenderer> ().enabled = false;
				flashLight.transform.GetChild (0).GetComponent<Light> ().enabled = false;
				binoculars.SetActive (false);
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha1)) // to select compass
			{
				compass.GetComponentInChildren<MeshRenderer> ().enabled = true;
				compass.transform.GetChild (3).GetComponentInChildren<MeshRenderer> ().enabled = true;
				flashLight.GetComponent<MeshRenderer> ().enabled = false;
				flashLight.transform.GetChild (0).GetComponent<Light> ().enabled = false;
				binoculars.SetActive (false);
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha2)) // to select flashlight
			{ 
				compass.GetComponentInChildren<MeshRenderer> ().enabled = false;
				compass.transform.GetChild (3).GetComponentInChildren<MeshRenderer> ().enabled = false;
				flashLight.GetComponent<MeshRenderer> ().enabled = true;
				flashLight.transform.GetChild (0).GetComponent<Light> ().enabled = true;
				binoculars.SetActive (false);
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            } 
			else if (Input.GetKeyDown (KeyCode.Alpha3)) // to select binoculars
			{ 
				compass.GetComponentInChildren<MeshRenderer> ().enabled = false;
				compass.transform.GetChild (3).GetComponentInChildren<MeshRenderer> ().enabled = false;
				flashLight.GetComponent<MeshRenderer> ().enabled = false;
				flashLight.transform.GetChild (0).GetComponent<Light> ().enabled = false;
				binoculars.SetActive (true);
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false; // update these latter for get component in children
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) // to select flare gun
            {
                compass.GetComponentInChildren<MeshRenderer>().enabled = false;
                compass.transform.GetChild(3).GetComponentInChildren<MeshRenderer>().enabled = false;
                flashLight.GetComponent<MeshRenderer>().enabled = false;
                flashLight.transform.GetChild(0).GetComponent<Light>().enabled = false;
                binoculars.SetActive(false);
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true; // update these latter for get component in children
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = true;
                this.transform.GetChild(4).transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
	}

	private void switchEnabled(bool value)
	{
		canSwitch = value;
	}

}
