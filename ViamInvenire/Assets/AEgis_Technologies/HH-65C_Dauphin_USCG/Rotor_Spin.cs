using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor_Spin : MonoBehaviour {

    public float rotationSpeed = 300.0f;
    private Vector3 rotationVector { get; set; }

	// Use this for initialization
	void Start () {
        rotationVector = transform.localRotation.eulerAngles;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rotationVector = new Vector3(rotationVector.x, rotationVector.y + rotationSpeed * Time.deltaTime, rotationVector.z);
        this.transform.eulerAngles = rotationVector;
	}
}
