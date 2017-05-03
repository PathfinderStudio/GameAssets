using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathScript : MonoBehaviour
{

    /// <summary>
    /// The death panel used to display that you have failed the game.
    /// </summary>
    public GameObject youDiedPanel;
    public AudioClip landingGrunt;
    public AudioClip breakingLegs;

    private AudioSource[] audioSources;
    private AudioSource audioSrc;
    private CharacterController character;
    private float deathVelocity;
    private bool grounded;
    private float characterYVelocity;
    private bool gonnnaDie;
    private bool willLoseStamina;
    private float staminaVelocity;

    private float invulnerabilityTime;
    private bool invulnerable = true;


    // Use this for initialization
    void Start()
    {
        audioSources = this.GetComponents<AudioSource>();
        audioSrc = audioSources[1];
        youDiedPanel.SetActive(false);
        character = this.GetComponent<CharacterController>();
        characterYVelocity = character.velocity.y;
        deathVelocity = -40.0f;
        grounded = false;
        gonnnaDie = false;
        willLoseStamina = false;
        staminaVelocity = -30.0f;

        invulnerabilityTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerable)
        {
            invulnerabilityTime += Time.deltaTime;
            if(invulnerabilityTime > 2.0f)
            {
                invulnerable = false;
            }
            gonnnaDie = false;
        }

        if(!invulnerable)
        {
            
            if (!grounded)
            {
                characterYVelocity = character.velocity.y;
                if (characterYVelocity < deathVelocity)
                {
                    gonnnaDie = true;
                }
                else if (characterYVelocity < staminaVelocity)
                {
                    willLoseStamina = true;
                }
            }
            else
            {
                if (willLoseStamina)
                {
                    willLoseStamina = false;
                    SendMessage("LostStamina", SendMessageOptions.DontRequireReceiver);
                }
                if (gonnnaDie)
                {
                    audioSources[0].Stop();
                    audioSrc.Stop();
                    audioSrc.clip = breakingLegs;
                    audioSrc.PlayOneShot(audioSrc.clip, 1.0f);
                    youDiedPanel.SetActive(true);
                    
                    //Time.timeScale = 0;
                }
            }
        }
    }

    public void OnLand()
    {
        grounded = true;
        
        audioSrc.clip = landingGrunt;
        audioSrc.Play();
    }
    public void OnFall()
    {
        grounded = false;
    }

    public void OnJump()
    {
        grounded = false;
    }

    public void KillPlayer()
    {
        if (!youDiedPanel.activeSelf)
        {
            gonnnaDie = true;
        }
    }
}
