using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRadio : MonoBehaviour {

    private static int radioCount = 0;
    private bool played;
    private AudioSource audioBox;
    public  AudioClip[] audioTracks = new AudioClip[10];
	// Use this for initialization
	void Start () {
        radioCount = 0;
        audioBox = this.GetComponent<AudioSource>();
        played = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (Time.timeScale != 0)
        {
            if (col.gameObject.tag == "Player")
            {
                //col.gameObject.SendMessage("Climbing", true, SendMessageOptions.DontRequireReceiver);
                //col.gameObject.SendMessage("addY", SendMessageOptions.DontRequireReceiver);
                if (!played && audioTracks[radioCount] != null)
                {
                    Debug.Log("RadioTrigger");
                    audioBox.clip = audioTracks[radioCount];
                    audioBox.Play();
                    radioCount++;
                    played = true;
                }
            }
        }
    }
}
