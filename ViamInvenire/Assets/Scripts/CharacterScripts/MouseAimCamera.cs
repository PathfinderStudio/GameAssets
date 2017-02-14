using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {

    // Use this for initialization
    // speed is the rate at which the object will rotate
    [Header("Look Sensitivity")]
    public float speedX = 100.0f;
    public float speedY = 100.0f;
    [Header("Vertical Look Constraints")]
    public float maxY = 70.0f;
    public float minY = -70.0f;

	private float lookVertical;
	private float lookHorizontal;

    private void Start()
    {
        if(GetComponent<Rigidbody>())
        {
            this.GetComponent<Rigidbody>().freezeRotation = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Based off mouselook script of GreenForest asset pack
    void Update()
    {
        if(this.tag == "Player")
        {
            lookHorizontal = this.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * speedX * Time.deltaTime;
            this.transform.localEulerAngles = new Vector3(0, lookHorizontal, 0);
        }
        else if(this.tag == "MainCamera")
        {
            lookVertical += Input.GetAxis("Mouse Y") * speedY * -1 * Time.deltaTime;
            lookVertical = Mathf.Clamp(lookVertical, minY, maxY);
            this.transform.localEulerAngles = new Vector3(lookVertical, 0, 0);
        }
		
        

		/*
		if (lookVertical.x > 0) // look down
		{
			if (Camera.transform.rotation.eulerAngles.x > 70 && Camera.transform.rotation.eulerAngles.x < 180) 
			{
				Camera.transform.eulerAngles = new Vector3 (70, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
			} 
		} 
		else // look up
		{
			if (Camera.transform.rotation.eulerAngles.x < 290 && Camera.transform.rotation.eulerAngles.x > 180) 
			{
				Camera.transform.eulerAngles = new Vector3 (290, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
			}
		}
        */
        
    }
}
