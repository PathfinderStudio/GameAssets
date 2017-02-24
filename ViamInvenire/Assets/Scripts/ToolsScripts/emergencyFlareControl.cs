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
    private Light lightComponent;
    private GameObject camera;
    private bool threwAFlare;
    private GameObject theFlareThrown;
    private Vector3 throwRotation;

    // Use this for initialization
    void Start()
    {
        amount = 3;
        lit = false;
        originalBurnTime = burnTime;
        lightComponent = this.transform.GetChild(0).GetComponent<Light>();
        lightComponent.enabled = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<CapsuleCollider>().enabled = false;
        threwAFlare = false;
        throwRotation = new Vector3(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(threwAFlare)
        {
            theFlareThrown.GetComponent<emergencyFlareControl>().SetThrownVariables(burnTime, burnRate, throwDistance);
            threwAFlare = false;
            theFlareThrown = null;
        }
        if(!lit && amount > 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            amount--;
            theFlareThrown = ThrowFlare();
            threwAFlare = true;
        }
        else if(lit && amount > 0 && Input.GetKeyUp(KeyCode.Mouse1))
        {
            lit = false;
            burnTime = 0;
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
            Destroy(this.gameObject);
        }
        else if(!lit && amount == 0)
        {
            Destroy(this.gameObject, 1f * Time.deltaTime);
        }
        else if(burnTime == 0 && amount > 0)
        {
            burnTime = originalBurnTime;
            lit = false;
        }

        if(amount < 0)
        {
            this.transform.Rotate(throwRotation);
            throwRotation.y += 10;
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
        lightComponent.enabled = true;
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
}
