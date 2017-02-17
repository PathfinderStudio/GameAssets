using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathScript : MonoBehaviour
{

    /// <summary>
    /// The death panel used to display that you have failed the game.
    /// </summary>
    public GameObject youDiedPanel;

    private CharacterController character;
    private float deathVelocity;
    private bool grounded;
    private float characterYVelocity;
    private bool gonnnaDie;
    private bool willLoseStamina;
    private float staminaVelocity;

    // Use this for initialization
    void Start()
    {
        youDiedPanel.SetActive(false);
        character = this.GetComponent<CharacterController>();
        characterYVelocity = character.velocity.y;
        deathVelocity = -40.0f;
        grounded = false;
        gonnnaDie = false;
        willLoseStamina = false;
        staminaVelocity = -30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!grounded)
        {
            characterYVelocity = character.velocity.y;
            if (characterYVelocity < deathVelocity)
            {
                gonnnaDie = true;
            }
            else if(characterYVelocity < staminaVelocity)
            {
                willLoseStamina = true;
            }
        }
        else
        {
            if(willLoseStamina)
            {
                willLoseStamina = false;
                SendMessage("LostStamina", SendMessageOptions.DontRequireReceiver);
            }
            if(gonnnaDie)
            {
                youDiedPanel.SetActive(true);
                //Time.timeScale = 0;
            }
        }
        
    }

    public void OnLand()
    {
        grounded = true;
    }
    public void OnFall()
    {
        grounded = false;
    }

    public void OnJump()
    {
        grounded = false;
    }
}
