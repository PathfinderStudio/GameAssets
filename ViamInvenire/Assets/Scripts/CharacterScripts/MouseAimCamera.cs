using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {

    // Use this for initialization
    // speed is the rate at which the object will rotate
    public float speed = 100.0f;
	public GameObject Camera;

	private Vector3 lookVertical;
	private Vector3 lookHorizontal;

    void FixedUpdate()
    {
		lookHorizontal.x = 0;
		lookHorizontal.y = Input.GetAxis ("Mouse X");
		lookHorizontal.z = 0;
		this.transform.Rotate (lookHorizontal, speed * Time.deltaTime);

		lookVertical.x = Input.GetAxis("Mouse Y") * -1;
		lookVertical.y = 0;
		lookVertical.z = 0;
		Camera.transform.Rotate(lookVertical, speed * Time.deltaTime);


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
        
    }
}
