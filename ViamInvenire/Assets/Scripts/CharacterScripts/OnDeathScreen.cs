using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDeathScreen : MonoBehaviour
{

    private float colorR;
    private float colorG;
    private float colorB;
    private float alpha;
    private bool fine;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0);
        colorR = 1.0f;
        colorG = 1.0f;
        colorB = 1.0f;
        alpha = 1.0f;
        fine = false;
    } 

    // Update is called once per frame
    void Update()
    {
        if(this.enabled && !fine)
        {
            this.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, alpha) * Time.deltaTime;
            
            if(this.GetComponent<Image>().color.a == 255.0f)
            {
                fine = true;
                Time.timeScale = 0;
            }

        }
    }


}
