using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectsScript : MonoBehaviour
{
    public GameObject glow;

    private float maxDist = 3f;
    private int compassIndex;
    private int binocularsIndex;
    private int flaregunIndex;
    private int flashlightIndex;
    private bool canPickup = false;
    private int itemToPickup;

    // Use this for initialization
    void Start()
    {
        glow.SetActive(true);
        glow.GetComponent<Light>().enabled = false;
        for(int i =0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            if (this.transform.GetChild(i).gameObject.tag == "Binoculars")
            {
                binocularsIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flaregun")
            {
                flaregunIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Flashlight")
            {
                flashlightIndex = i;
            }
            else if (this.transform.GetChild(i).gameObject.tag == "Compass")
            {
                compassIndex = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        glow.SetActive(true);
        RaycastHit hit;
        int mask = 1;
        int layer = mask << LayerMask.NameToLayer("Tools");
        //Vector3 viewportPosition = Camera.main.WorldToViewportPoint(new Vector3(Screen.width/2, Screen.height/2, 0));
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit, maxDist, layer))
        {
            if(hit.collider.gameObject.tag == "Binoculars")
            {
                canPickup = true;
                itemToPickup = binocularsIndex;
            }
            else if(hit.collider.gameObject.tag == "Flaregun")
            {
                canPickup = true;
                itemToPickup = flaregunIndex;

            }
            else if (hit.collider.gameObject.tag == "Flashlight")
            {

                canPickup = true;
                itemToPickup = flashlightIndex;

            }
            else if (hit.collider.gameObject.tag == "Compass")
            {
                canPickup = true;
                itemToPickup = compassIndex;
            }

            glow.GetComponent<Light>().enabled = true;
            glow.transform.position = hit.collider.gameObject.transform.position + this.transform.forward/10;
            glow.GetComponent<Light>().intensity = 1.0f;
        }
        else
        {
            canPickup = false;
            glow.GetComponent<Light>().enabled = false;
        }

        if(canPickup && (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E)))
        {
            this.transform.GetChild(itemToPickup).gameObject.SetActive(true);
            //Change this script to address tool object prototype and set their bool
            //to true that it has been obtained
        }
    }
}
