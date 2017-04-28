using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopThroughSounds : MonoBehaviour {

    public GameObject player;

    private int numSounds;
	// Use this for initialization
	void Start ()
    {
        numSounds = this.gameObject.transform.childCount;
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < numSounds; i++)
        {
            GameObject currentSound = this.gameObject.transform.GetChild(i).gameObject;
            float distance = currentSound.GetComponent<AudioSource>().maxDistance;
            if (Vector3.Distance(player.transform.position, currentSound.transform.position) > distance)
            {
                currentSound.SetActive(false);
            }
            else
            {
                currentSound.SetActive(true);
            }
        }
	}
}
