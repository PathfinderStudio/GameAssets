using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAudioListener : MonoBehaviour {

    public AudioListener audioListener;
    private float timer = 0.0f;
    private bool tripped = false;
	// Use this for initialization
	void Start ()
    {
        //audioListener = GameObject.FindObjectOfType<AudioListener>();
        audioListener.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < 0.5f && !tripped)
        {
            timer += Time.deltaTime;
        }
        else if(timer > 0.5f && !tripped)
        {
            tripped = true;
            audioListener.gameObject.SetActive(true);
        }
	}
}
