using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameTimer : MonoBehaviour
{
    [Header("Time Until Game Ends")]
    public int minutesToEscape = 15;

    [Header("Canvas Elements For Game End")]
    public GameObject timeEndPanel;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject quitButton;

    private GameObject character;
    private int minutesPassed;
    private int secondsPassed;
    private Timer timerTillEnd;
    private double tickAmount;
    private float alphaChangeAmount;
    private bool ended;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        timeEndPanel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0);
        timeEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(timeEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.r, timeEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.g, timeEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.b, 0);
        alphaChangeAmount = 2.0f;
        timeEndPanel.SetActive(false);
        restartButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);
        minutesPassed = 0;
        secondsPassed = 0;
        tickAmount = 1000;
        timerTillEnd = new Timer(tickAmount);
        timerTillEnd.Elapsed += OnTimedEvent;
        timerTillEnd.AutoReset = true;
        timerTillEnd.Enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (minutesPassed >= minutesToEscape)
        {
            timerTillEnd.AutoReset = false;
            timerTillEnd.Stop();
            EndGame();
        }
        if (ended)
        {
            if(timeEndPanel.GetComponent<Image>().color.a < 1.0f)
            {
                timeEndPanel.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, alphaChangeAmount) * Time.deltaTime;
                timeEndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color += new Color(0.0f, 0.0f, 0.0f, alphaChangeAmount) * Time.deltaTime;
            }
            else
            {
                restartButton.SetActive(true);
                mainMenuButton.SetActive(true);
                quitButton.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
        }
    }

    private void EndGame()
    {
        ended = true;
        timeEndPanel.SetActive(true);
    }
    

    /// <summary>
    /// Event handler for timer ticking each second.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="elapse"></param>
    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        if(secondsPassed < 60)
        {
            secondsPassed++;
        }
        else
        {
            secondsPassed = 0;
            minutesPassed++;
        }
    }

    private void ResetOnRestart()
    {
        minutesPassed = 0;
        secondsPassed = 0;
        timerTillEnd.Stop();
        timerTillEnd.Dispose();
        timerTillEnd = new Timer(tickAmount);
    }

    //need to receive when helicopter is called so that the timer doesn't keep going
    public void GameWon()
    {
        minutesPassed = 0;
        secondsPassed = 0;
        timerTillEnd.Stop();
        timerTillEnd.Dispose();
        ended = false;
    }
}
