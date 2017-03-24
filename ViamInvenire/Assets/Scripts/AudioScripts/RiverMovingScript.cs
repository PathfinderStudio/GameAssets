using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMovingScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1332.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);

        if (player.transform.position.z > 1675)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1675f);
        }

        else if (player.transform.position.z < 990)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 990f);
        }
    }
}
