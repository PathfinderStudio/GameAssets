using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtons : MonoBehaviour
{

    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject quitGameButton;

    // Use this for initialization
    void Start()
    {
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitGameButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
