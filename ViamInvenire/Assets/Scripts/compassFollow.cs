using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compassFollow : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		Vector3 compassPos = this.transform.position; //compass position in world

		Vector3 ballPos = target.transform.position; //mouse position in world

		float angle = Mathf.Atan2 (compassPos.z - ballPos.z, compassPos.x - ballPos.x) * Mathf.Rad2Deg; // angle between them

		this.transform.rotation = Quaternion.Euler (new Vector3 (90f, 0f, angle + 90)); //rotating it wrong but at least rotating
    }
}
