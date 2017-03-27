using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStamina : MonoBehaviour
{
    /// <summary>
    /// How long the player can run before running out of stamina.
    /// Larger numbers mean longer run time.
    /// </summary>
    public float runTime = 7.0f;
    public float recoveryRate = 1f;
    public float burnRate = 1f;

    private float currentTimeRunning;
    private bool tired;
    private bool running;
    private float minimumThreshold;
    private Rect staminaRect;
    private Texture2D staminaTexture;
    private bool canMove = true;


    // Use this for initialization
    void Start()
    {
        currentTimeRunning = 0;
        tired = false;
        running = false;
        minimumThreshold = 3.5f;

        //for drawing stamina bar
        staminaRect = new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 20, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.green);
        staminaTexture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if the player is running
        if (!tired && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && canMove)
        {
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) //checks that you are actualy moving to set running to true
            {
                running = false;
            }
            else
            {
                running = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            running = false;
        }
        if (tired || !canMove)
        {
            running = false;
        }
        //If the player is running bring down his stamina
        if (Input.GetKeyDown(KeyCode.Space) && running)
        {
            currentTimeRunning += burnRate * Time.deltaTime * 10; //this is a test to make jumping burn more stamina
        }
        else if (running)
        {
            currentTimeRunning += burnRate * Time.deltaTime;
        }
        else
        {
            currentTimeRunning -= recoveryRate * Time.deltaTime;

            if (currentTimeRunning < 0.0f)
            {
                currentTimeRunning = 0.0f;
            }
        }

        if (!tired && currentTimeRunning >= runTime)
        {
            tired = true;
            SendMessage("SetCanRun", false, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            if (!running && currentTimeRunning <= minimumThreshold)
            {
                tired = false;
                SendMessage("SetCanRun", true, SendMessageOptions.DontRequireReceiver);
            }
        }

    }

    /// <summary>
    /// Hit the ground from too high up and caused the player to lose their stamina
    /// </summary>
    public void LostStamina()
    {
        currentTimeRunning = runTime;
        tired = true;
        SendMessage("SetCanRun", false, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Determines whether or not the player is allowed to move.
    /// </summary>
    /// <param name="val"></param>
    public void SetCanMove(bool val)
    {
        canMove = val;
    }

    private void OnGUI()
    {
        float amountOfStam = (runTime - currentTimeRunning) / runTime;
        if (amountOfStam < 1)
        {
            float width = amountOfStam * Screen.width / 3;
            staminaRect.width = width;
            GUI.DrawTexture(staminaRect, staminaTexture);
        }
        else
        {
            GUI.DrawTexture(Rect.zero, staminaTexture);
        }

    }
}
