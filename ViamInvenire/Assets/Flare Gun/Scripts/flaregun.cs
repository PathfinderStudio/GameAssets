using UnityEngine;
using System.Collections;

public class flaregun : MonoBehaviour
{

    public Rigidbody flareBullet;
    public Transform barrelEnd;
    public GameObject muzzleParticles;
    public AudioClip flareShotSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;
    public int bulletSpeed = 2000;
    public int maxSpareRounds = 5;
    public int spareRounds = 3;
    public int currentRound = 0;

    private GameObject GameManager;

    private bool playerHolding = false;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying)
        {
            if (currentRound > 0)
            {
                Shoot();
                GameManager.SendMessage("ActivateRescueHelicopter", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Calling helicopter script");
            }
            else
            {
                GetComponent<Animation>().Play("noAmmo");
                GetComponent<AudioSource>().PlayOneShot(noAmmoSound);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !GetComponent<Animation>().isPlaying)
        {
            Reload();
        }
    }

    void Shoot()
    {
        currentRound--;
        if (currentRound <= 0)
        {
            currentRound = 0;
        }

        GetComponent<Animation>().CrossFade("Shoot");
        GetComponent<AudioSource>().PlayOneShot(flareShotSound);


        Rigidbody bulletInstance;
        bulletInstance = Instantiate(flareBullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE


        bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE

        Instantiate(muzzleParticles, barrelEnd.position, barrelEnd.rotation);   //INSTANTIATING THE GUN'S MUZZLE SPARKS	

    }

    void Reload()
    {
        if (spareRounds >= 1 && currentRound == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(reloadSound);
            spareRounds--;
            currentRound++;
            GetComponent<Animation>().CrossFade("Reload");
        }

    }

    public void itemPickedUp(bool input)
    {
        playerHolding = input;
    }

    public bool isPlayerHolding()
    {
        return playerHolding;
    }
}
