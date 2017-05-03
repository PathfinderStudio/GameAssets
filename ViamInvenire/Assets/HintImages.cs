using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintImages : MonoBehaviour {
    static int numImages = 5;
    public Sprite[] hints = new Sprite[numImages];
    int current = 0;
    // Use this for initialization
    void Start () {
        this.transform.GetChild(0).GetComponent<Image>().sprite = hints[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void hide()
    {
        this.gameObject.SetActive(false);
    }

    void show()
    {
        gameObject.SetActive(true);
    }

    public void change(int dir)
    {
        current += dir;
        if(current < 0)
        {
            current = numImages - 1;
        }
        else if(current == numImages)
        {
            current = 0;
        }

        this.transform.GetChild(0).GetComponent<Image>().sprite = hints[current];
        
    }
}
