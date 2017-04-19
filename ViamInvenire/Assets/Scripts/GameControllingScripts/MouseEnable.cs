using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnable : MonoBehaviour {
    
    private AudioSource menuMusic;
	// Use this for initialization
	void Start () {
        menuMusic = this.GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuMusic.PlayDelayed(1.0f);
    }
}
