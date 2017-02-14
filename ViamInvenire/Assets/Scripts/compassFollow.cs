using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compassFollow : MonoBehaviour {

	public GameObject target; // the target object
	public GameObject firstPersonController;

	// Update is called once per frame
	void Update ()
    {
		target.transform.position = new Vector3 (firstPersonController.transform.position.x, target.transform.position.y, target.transform.position.z); // sets the x pos to the camera x pos so it follows it parallel along the x-axis

		Vector3 compassPos = this.transform.position; //compass position in world

		Vector3 targetPos = target.transform.position; //mouse position in world

		float angle = Mathf.Atan2 (compassPos.z - targetPos.z, compassPos.x - targetPos.x) * Mathf.Rad2Deg; // angle between them

		this.transform.rotation = Quaternion.Euler (new Vector3 (0f, angle, 0f)); // rotates it so it points to that position
    }
}
