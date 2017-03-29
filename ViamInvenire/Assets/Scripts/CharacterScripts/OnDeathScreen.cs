using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDeathScreen : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject quitGameButton;

    private float alpha;
    private bool fine;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0);
        this.transform.GetChild(0).GetComponent<Text>().enabled = false;
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitGameButton.SetActive(false);
        alpha = 1.0f;
        fine = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.enabled && !fine)
        {
            this.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, alpha);
            if (this.GetComponent<Image>().color.a > 1.0f)
            {
                fine = true;
                this.transform.GetChild(0).GetComponent<Text>().enabled = true;
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
                quitGameButton.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }


        }
    }


}
