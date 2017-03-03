using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class binocularControl : MonoBehaviour {

	public GameObject view;
	public Camera binocularCamera;
	public float sensitivity;
	public GameObject mainCharacter;
    public float maxZoomOut = 60f;
    public float maxZoomIn = 5f;
    public float minSensitivity = 40;

    private MouseAimCamera aimCamera;
    private float sensitivityChangeAmount;
    private float scrollValue;
	private float maxSensitivity;
    private bool playerHolding = false;
    // Use this for initialization
    void Start () 
	{
        aimCamera = binocularCamera.transform.parent.transform.parent.GetComponent<MouseAimCamera>();
        maxSensitivity = aimCamera.speedX;
        sensitivityChangeAmount = (maxSensitivity - minSensitivity) / (maxZoomOut - maxZoomIn);
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) // right mouse click
		{
			if (view.gameObject.activeInHierarchy) 
			{
                aimCamera.speedX = maxSensitivity; //resets sensitivity when changing back to normal camera
                aimCamera.speedY = maxSensitivity;
                view.gameObject.SetActive (false);
				binocularCamera.enabled = false;
				mainCharacter.SendMessage ("switchEnabled", true, SendMessageOptions.DontRequireReceiver);
                mainCharacter.SendMessage("SetCanMove", true, SendMessageOptions.DontRequireReceiver);
			} 
			else
			{
				view.gameObject.SetActive (true);
				binocularCamera.enabled = true;
				mainCharacter.SendMessage ("switchEnabled", false, SendMessageOptions.DontRequireReceiver);
                mainCharacter.SendMessage("SetCanMove", false, SendMessageOptions.DontRequireReceiver);
            }
		}

		scrollValue = Input.GetAxis ("Mouse ScrollWheel");
		if (binocularCamera.enabled) 
		{
			if (scrollValue > 0f) // if positive scroll value(scroll up) then the camera zooms in
			{
                aimCamera.speedX -= scrollValue * sensitivity * sensitivityChangeAmount;
                if (aimCamera.speedX < minSensitivity) // min sensitivity allowed is 40
                {
                    aimCamera.speedX = minSensitivity;
                }
                aimCamera.speedY = aimCamera.speedX;

                binocularCamera.fieldOfView -= scrollValue * sensitivity;
				if (binocularCamera.fieldOfView < maxZoomIn) // max zoom in allowed is 5
				{
					binocularCamera.fieldOfView = maxZoomIn;
				}
			} 
			else // if negative scroll value(scroll down) then the camera zooms out
			{
                aimCamera.speedX += scrollValue * sensitivity * -1 * sensitivityChangeAmount; // multiply by negative 1 to get value of change so we can add or subtract non negative nums
                if (aimCamera.speedX > maxSensitivity) // max sensitivity allowed is 100
                {
                    aimCamera.speedX = maxSensitivity;
                }
                aimCamera.speedY = aimCamera.speedX;

                binocularCamera.fieldOfView += scrollValue * sensitivity * -1; // multiply by negative 1 to get value of change so we can add or subtract non negative nums
				if (binocularCamera.fieldOfView > maxZoomOut) // max zoom out allowed is 60
				{
					binocularCamera.fieldOfView = maxZoomOut;
				}
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
