using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class emergencyFlareControl : MonoBehaviour
{
    public GameObject flareToThrow;
    //Try to set to actual time
    [Header("The Larger the Value, the Longer the Flare Lasts")]
    public float burnTime = 1000f;
    [Header("The Larger the Value, the Shorter the Flare Lasts")]
    public float burnRate = 10f;
    public float throwDistance = 2000.0f;
    public GameObject iconCounter;


    private float originalBurnTime;
    private int amount;
    private bool lit;
    private GameObject lightComponent;
    private GameObject camera;
    private bool threwAFlare;
    private GameObject theFlareThrown;
    private Vector3 throwRotation;
    private float rotationAmount;
    private bool playerHolding;
    private bool flying;
    private int bounceCount;
    private AudioSource soundSource;


    // Use this for initialization
    void Start()
    {
        amount = 3;
        lit = false;
        originalBurnTime = burnTime;
        lightComponent = this.transform.GetChild(0).gameObject;
        lightComponent.SetActive(false);
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<CapsuleCollider>().enabled = false;
        threwAFlare = false;
        throwRotation = new Vector3(0, 0, 0);
        rotationAmount = 10f;
        playerHolding = true;
        flying = false;
        bounceCount = 0;
        soundSource = this.GetComponent<AudioSource>();
        soundSource.playOnAwake = false;
        iconCounter.SendMessage("SetAmount", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {

        if (threwAFlare)
        {
            theFlareThrown.GetComponent<emergencyFlareControl>().SetThrownVariables(burnTime, burnRate, throwDistance);
            burnTime = 0;
            threwAFlare = false;
            theFlareThrown = null;
            lit = false;
            
        }
        if (!lit && amount > 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            amount--;
            iconCounter.SendMessage("DecrementAmount", SendMessageOptions.DontRequireReceiver);
            lit = true;
            theFlareThrown = ThrowFlare();
            threwAFlare = true;
            
        }
        else if (lit && amount > 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            lit = false;
            burnTime = 0;
            theFlareThrown = ThrowFlare();
            threwAFlare = true;

        }
        else if (lit && amount == 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            lit = false;
            //burnTime = 0;
            theFlareThrown = ThrowFlare();
            threwAFlare = true;

        }
        if (!lit && amount > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Application.OpenURL("https://youtu.be/U1ei5rwO7ZI"); //great song
            amount--;
            iconCounter.SendMessage("DecrementAmount", SendMessageOptions.DontRequireReceiver);
            lit = true;
            LightFlare();
        }
        else if (lit)
        {
            burnTime -= burnRate * Time.deltaTime;
        }

        if (burnTime < 0 && amount <= 0)
        {
            itemPickedUp(false);
            this.gameObject.SetActive(false);


        }
        else if (!lit && amount == 0 && threwAFlare)
        {
            //Do something? Donno
        }
        else if (!lit && amount == 0)
        {
            itemPickedUp(false);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject, 1f * Time.deltaTime);

        }
        else if (burnTime == 0 && amount > 0)
        {
            burnTime = originalBurnTime;
            lit = false;
            lightComponent.SetActive(false);
        }

        if (flying)
        {
            this.transform.eulerAngles = throwRotation;
            throwRotation.x += rotationAmount;
        }
        if (bounceCount > 0)
        {
            SlowDown();
        }
    }

    /// <summary>
    /// Create an image at the location of where the flare lands.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator makeImage(string url)
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(tex);
        this.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = tex;
    }

    /// <summary>
    /// Create a sound at the location of where the flare flys.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private void playSound()
    {
        //string url
        //WWW www = new WWW(url);
        soundSource.loop = true;
        //soundSource.clip = www.GetAudioClip(true, false);
        soundSource.time = 0.81f * soundSource.clip.length;
        soundSource.Play();
    }

    /// <summary>
    /// Method to throw a flare object.
    /// </summary>
    private GameObject ThrowFlare()
    {
        GameObject thrownFlare = Instantiate(flareToThrow, this.transform.position + this.transform.parent.transform.parent.GetComponent<Collider>().bounds.size / 2, Quaternion.identity);
        return thrownFlare;
    }

    /// <summary>
    /// Method to turn on flare's light component.
    /// </summary>
    private void LightFlare()
    {
        lightComponent.SetActive(true);
    }

    /// <summary>
    /// Set values for the thrown flare.
    /// </summary>
    /// <param name="oldBurnTime">How long the parent flare burned for.</param>
    /// <param name="oldBurnRate">How fast the parent flare burned.</param>
    /// <param name="oldThrowDistance">How far the parent flare flew.</param>
    private void SetThrownVariables(float oldBurnTime, float oldBurnRate, float oldThrowDistance)
    {
        lit = true;
        burnTime = oldBurnTime;
        burnRate = oldBurnRate;
        amount = -1;
        throwDistance = oldThrowDistance;
        LightFlare();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Vector3 throwDirection = camera.transform.forward;
        this.GetComponent<Rigidbody>().AddForce(throwDirection * throwDistance);
        this.GetComponent<CapsuleCollider>().enabled = true;
        flying = true;
        playSound();
    }

    public void itemPickedUp(bool input)
    {
        playerHolding = input;
        if (playerHolding)
        {
            IncreaseAmountOfFlares();
        }
    }

    public bool isPlayerHolding()
    {
        return playerHolding;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rotationAmount = 0;
            throwRotation = Vector3.zero;
            flying = false;
            bounceCount++;
            //StartCoroutine(makeImage("http://www.pageresource.com/png/var/albums/nature/fire/big-fire-ball-explosion-png-image.png"));
        }
    }

    /// <summary>
    /// Increase number of flares being carried.
    /// </summary>
    private void IncreaseAmountOfFlares()
    {
        amount++;
        iconCounter.SendMessage("IncrementAmount", SendMessageOptions.DontRequireReceiver);

    }

    void SlowDown()
    {
        this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity - this.GetComponent<Rigidbody>().velocity * Time.deltaTime;
        if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.magnitude) < 0.01)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}