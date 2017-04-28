using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMovingScript : MonoBehaviour {

    public GameObject player;
    public float maxZ;
    public float minZ;

    public float maxX;
    public float minX;

	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1332.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);

        if (player.transform.position.z > maxZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, maxZ);
        }

        else if (player.transform.position.z < minZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, minZ);
        }

        if(player.transform.position.x > maxX)
        {
            this.transform.position = new Vector3(maxX, this.transform.position.y, this.transform.position.z);
        }
        else if(player.transform.position.x < minX)
        {
            this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
        }
    }
}
