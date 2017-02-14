using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class binocularControl : MonoBehaviour {

	public GameObject view;
	public Camera binocularCamera;
	public float sensitivity;

	public GameObject mainCharacter;

	private float scrollValue;
	private int maxZoomOut;
	private int maxZoomIn;
	// Use this for initialization
	void Start () 
	{
		maxZoomOut = 60;
		maxZoomIn = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) // right mouse click
		{
			if (view.gameObject.activeInHierarchy) 
			{
				view.gameObject.SetActive (false);
				binocularCamera.enabled = false;
				mainCharacter.SendMessage ("switchEnabled", true, SendMessageOptions.DontRequireReceiver);
			} 
			else
			{
				view.gameObject.SetActive (true);
				binocularCamera.enabled = true;
				mainCharacter.SendMessage ("switchEnabled", false, SendMessageOptions.DontRequireReceiver);
			}
		}

		scrollValue = Input.GetAxis ("Mouse ScrollWheel");
		if (binocularCamera.enabled) 
		{
			if (scrollValue > 0f) // if positive scroll value(scroll up) then the camera zooms in
			{
				binocularCamera.fieldOfView -= scrollValue * sensitivity;
				if (binocularCamera.fieldOfView < maxZoomIn) // max zoom in allowed is 5
				{
					binocularCamera.fieldOfView = maxZoomIn;
				}
			} 
			else // if negative scroll value(scroll down) then the camera zooms out
			{ 
				binocularCamera.fieldOfView += scrollValue * sensitivity * -1; // multiply by negative 1 to get value of change so we can add or subtract non negative nums
				if (binocularCamera.fieldOfView > maxZoomOut) // max zoom out allowed is 60
				{
					binocularCamera.fieldOfView = maxZoomOut;
				}
			}
		}


	}
}
