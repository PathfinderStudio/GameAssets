using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter_Fly_Straight : MonoBehaviour {

    public float Speed = 100f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += -1f*(transform.forward * Time.deltaTime * Speed);
    }
}
