using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emergencyFlareControl : MonoBehaviour
{
    public GameObject flareToThrow;
    public float burnTime = 1000f;
    public float burnRate = 10f;
    public float throwDistance = 2000.0f;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(threwAFlare)
        {
            theFlareThrown.GetComponent<emergencyFlareControl>().SetThrownVariables(burnTime, burnRate, throwDistance);
            burnTime = 0;
            threwAFlare = false;
            theFlareThrown = null;
        }
        if (!lit && amount > 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            amount--;
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
        if(!lit && amount > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            amount--;
            lit = true;
            LightFlare();
        }
        else if(lit)
        {
            burnTime -= burnRate * Time.deltaTime;
        }

        if(burnTime < 0 && amount <= 0)
        {
            itemPickedUp(false);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            
        }
        else if(!lit && amount == 0)
        {
            itemPickedUp(false);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject, 1f * Time.deltaTime);

        }
        else if(burnTime == 0 && amount > 0)
        {
            burnTime = originalBurnTime;
            lit = false;
            lightComponent.SetActive(false);
        }

        if(amount < 0)
        {
            this.transform.eulerAngles = throwRotation;
            throwRotation.x += rotationAmount;
        }
    }

    

    /// <summary>
    /// Method to throw a flare object.
    /// </summary>
    private GameObject ThrowFlare()
    {
        GameObject thrownFlare = Instantiate(flareToThrow, this.transform.position + this.transform.parent.transform.parent.GetComponent<Collider>().bounds.size/2, Quaternion.identity);
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
    }

    private void itemPickedUp(bool input)
    {
        playerHolding = input;
    }

    public bool isPlayerHolding()
    {
        return playerHolding;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            rotationAmount = 0;
        }
    }
}
