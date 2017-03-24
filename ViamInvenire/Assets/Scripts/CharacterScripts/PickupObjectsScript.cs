using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectsScript : MonoBehaviour
{
    public GameObject glow;

    private toolControl toolControl;
    public GameObject InventoryUI;

    private float maxDist = 3f;
    private int compassIndex;
    private int binocularsIndex;
    private int flaregunIndex;
    private int flashlightIndex;
    private int flareIndex;
    private bool canPickup = false;
    private int itemToPickup;
    private bool lightUp;

    // Use this for initialization
    void Start()
    {
        toolControl = this.gameObject.GetComponent<toolControl>();
        glow.SetActive(true);
        glow.GetComponent<Light>().enabled = false;
        lightUp = false;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).gameObject.tag != "Untagged")
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
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
            else if (this.transform.GetChild(i).gameObject.tag == "Flare")
            {
                flareIndex = i;
                this.transform.GetChild(i).gameObject.SetActive(true);
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
        float radius = 1.0f;
        //Physics.Raycast(ray, out hit, maxDist, layer)
        if (Physics.CapsuleCast(this.transform.position, this.transform.position, radius, this.transform.forward, out hit, maxDist, layer))
        {
            if (hit.collider.gameObject.tag == "Binoculars")
            {
                canPickup = true;
                itemToPickup = binocularsIndex;
                lightUp = true;
            }
            else if (hit.collider.gameObject.tag == "Flaregun")
            {
                canPickup = true;
                itemToPickup = flaregunIndex;
                lightUp = true;
            }
            else if (hit.collider.gameObject.tag == "Flashlight")
            {

                canPickup = true;
                itemToPickup = flashlightIndex;
                lightUp = true;
            }
            else if (hit.collider.gameObject.tag == "Compass")
            {
                canPickup = true;
                itemToPickup = compassIndex;
                lightUp = true;
            }
            else if (hit.collider.gameObject.tag == "Flare")
            {
                if (hit.collider.gameObject.GetComponent<emergencyFlareControl>() != null)
                {
                    canPickup = false;
                    glow.GetComponent<Light>().enabled = false;
                    lightUp = false;
                }
                else
                {
                    canPickup = true;
                    itemToPickup = flareIndex;
                    lightUp = true;
                }

            }
            if (lightUp)
            {
                glow.GetComponent<Light>().enabled = true;
                glow.transform.position = hit.collider.gameObject.transform.position + this.transform.forward / 10;
                glow.GetComponent<Light>().intensity = 1.0f;
            }

        }
        else
        {
            canPickup = false;
            glow.GetComponent<Light>().enabled = false;
            lightUp = false;
        }

        if (canPickup && (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E)))
        {
            this.transform.GetChild(itemToPickup).gameObject.SetActive(true);
            if (this.transform.GetChild(itemToPickup).tag == "Compass") //use compass
            {
                toolControl.switchItems(true, false, false, false, false, false);
                this.transform.GetChild(itemToPickup).GetChild(3).SendMessage("itemPickedUp", true, SendMessageOptions.DontRequireReceiver);
            }
            else if (this.transform.GetChild(itemToPickup).tag == "Binoculars") //using binoculars
            {
                toolControl.switchItems(false, false, false, true, false, false);
            }
            else if (this.transform.GetChild(itemToPickup).tag == "Flashlight") //using flashlight
            {
                toolControl.switchItems(false, true, true, false, false, false);
            }
            else if (this.transform.GetChild(itemToPickup).tag == "Flaregun") //using flaregun
            {
                toolControl.switchItems(false, false, false, false, true, false);
            }
            else if (this.transform.GetChild(itemToPickup).tag == "Flare") //use flare
            {
                toolControl.switchItems(false, false, false, false, false, true);
            }

            this.transform.GetChild(itemToPickup).SendMessage("itemPickedUp", true, SendMessageOptions.DontRequireReceiver);
            InventoryUI.GetComponent<InventoryUI>().AddItem(hit.collider.gameObject);
            for (int i = 0; i < hit.transform.childCount; i++)
            {
                if (hit.transform.GetChild(i).tag == "TutorialText")
                {
                    hit.transform.GetChild(i).SendMessage("UpdateTutorialText", SendMessageOptions.DontRequireReceiver);
                }
            }
            Destroy(hit.collider.gameObject);

            //Change this script to address tool object prototype and set their bool
            //to true that it has been obtained
        }
    }
}