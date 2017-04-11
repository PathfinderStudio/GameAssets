using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillUnderWater : MonoBehaviour
{
    public float drowningLimit = 3.0f;
    public GameObject drowningTimerPanel;

    private CharacterDeathScript toKill;
    private float drowningTimer;
    private float alphaRatio;

    // Use this for initialization
    void Start()
    {
        toKill = this.GetComponent<CharacterDeathScript>();
        drowningTimer = 0.0f;
        drowningTimerPanel.SetActive(true);
        drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, 0);
        alphaRatio = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < 8)
        {
            if(this.transform.position.y < 7)
            {
                drowningTimer = drowningLimit + 1;
            }
            drowningTimer += Time.deltaTime;
            alphaRatio = drowningTimer / drowningLimit * 255.0f;
            drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, alphaRatio);
            if (drowningTimer > drowningLimit)
            {
                drowningTimerPanel.SetActive(false);
                toKill.KillPlayer();
            }
        }
        else
        {
            drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, 0);
            drowningTimer = 0.0f;
        }
    }
}
