using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compassFollow : MonoBehaviour {

	public GameObject target;
	public GameObject firstPersonController;

	// Update is called once per frame
	void Update ()
    {
		target.transform.position = new Vector3 (firstPersonController.transform.position.x, target.transform.position.y, target.transform.position.z);

		Vector3 compassPos = this.transform.position; //compass position in world

		Vector3 targetPos = target.transform.position; //mouse position in world

		float angle = Mathf.Atan2 (compassPos.z - targetPos.z, compassPos.x - targetPos.x) * Mathf.Rad2Deg; // angle between them

		this.transform.rotation = Quaternion.Euler (new Vector3 (0f, angle, 0f)); //rotating it wrong but at least rotating
    }
}
