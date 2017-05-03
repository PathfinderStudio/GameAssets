using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillUnderWater : MonoBehaviour
{
    public GameObject deathPanel;
    public float drowningLimit = 3.0f;
    public GameObject drowningTimerPanel;

    private bool playerDead;
    private CharacterDeathScript toKill;
    private float drowningTimer;
    private float alphaRatio;

    // Use this for initialization
    void Start()
    {
        playerDead = false;
        toKill = this.GetComponent<CharacterDeathScript>();
        drowningTimer = 0.0f;
        drowningTimerPanel.SetActive(true);
        drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, 0);
        drowningTimerPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        alphaRatio = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < 7.8f)
        {
            if(this.transform.position.y < 7.0f)
            {
                drowningTimer = drowningLimit + 1;
            }
            drowningTimer += Time.deltaTime;
            alphaRatio = drowningTimer / drowningLimit;
            drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, alphaRatio);
            drowningTimerPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(alphaRatio, alphaRatio, alphaRatio, alphaRatio);
            if(deathPanel.activeSelf)
            {
                playerDead = true;
            }
            if (drowningTimer > drowningLimit && !playerDead)
            {
                
                playerDead = true;
                toKill.KillPlayer();
                drowningTimerPanel.SetActive(false);
            }
        }
        else
        {
            drowningTimerPanel.GetComponent<Image>().color = new Color(drowningTimerPanel.GetComponent<Image>().color.r, drowningTimerPanel.GetComponent<Image>().color.g, drowningTimerPanel.GetComponent<Image>().color.b, 0);
            drowningTimerPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            drowningTimer = 0.0f;
        }
    }
}
