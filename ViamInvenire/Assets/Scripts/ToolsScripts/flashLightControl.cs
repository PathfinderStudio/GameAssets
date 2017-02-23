using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashLightControl : MonoBehaviour {

	public GameObject camera;
	public GameObject player;
	public int scrollAmount;

	private int maxIntensity;
	private int minIntensity;
	private int maxSpotAngle;
	private int minSpotAngle;
	private Light flashLight;
	private float scrollValue;
    private bool playerHolding = false;

    // Use this for initialization
    void Start()
	{
		maxIntensity = 6;
		minIntensity = 2;
		maxSpotAngle = 100;
		minSpotAngle = 20;
		flashLight = this.gameObject.transform.GetChild (0).GetComponent<Light> (); // the light component of the flashlight object
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.rotation = Quaternion.Euler (new Vector3 (camera.transform.rotation.eulerAngles.x + 90, camera.transform.rotation.eulerAngles.y, camera.transform.rotation.eulerAngles.z)); // for the flashlight to point up or down following camera

		scrollValue = Input.GetAxis ("Mouse ScrollWheel");

		if (flashLight.enabled)
		{
			adjustLight (); 
		}

        if (Input.GetKeyDown(KeyCode.Mouse0) && this.gameObject.GetComponent<MeshRenderer>().enabled)
        {
            flashLight.enabled = !flashLight.enabled;
        }
    }

	/// <summary>
	/// Adjusts the light intensity and spotangle.
	/// </summary>
	private void adjustLight() 
	{
		if (scrollValue > 0f) // if positive scroll value(scroll up) then intensity increases and angle decreases
		{
			flashLight.intensity += scrollValue * scrollAmount;
			flashLight.spotAngle -= scrollValue * scrollAmount * 10;
			if (flashLight.intensity > maxIntensity) // max intensity allowed is 6
			{
				flashLight.intensity = maxIntensity;
			}
			if (flashLight.spotAngle < minSpotAngle) // lowest spot angle allowed is 20
			{
				flashLight.spotAngle = minSpotAngle;
			}
		} 
		else // if negative scroll value(scroll down) then intensity decreases and angle increases
		{ 
			flashLight.intensity -= scrollValue * scrollAmount * -1; // multiply by negative 1 to get value of change so we can add or subtract non negative nums
			flashLight.spotAngle += scrollValue * scrollAmount * 10 * -1;
			if (flashLight.intensity < minIntensity) // lowest intensity allowed is 2
			{
				flashLight.intensity = minIntensity;
			}
			if (flashLight.spotAngle > maxSpotAngle) // highest spot angle allowed is 100
			{
				flashLight.spotAngle = maxSpotAngle;
			}
		}
	}

    private void itemPickedUp(bool input)
    {
        playerHolding = input;
    }

    public bool isPlayerHolding()
    {
        return playerHolding;
    }
}
