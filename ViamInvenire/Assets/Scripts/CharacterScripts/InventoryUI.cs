using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public Image[] inventoryImages = new Image[numInventorySlots];
    public GameObject[] InventorySlots = new GameObject[numInventorySlots];
    public const int numInventorySlots = 5;

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < numInventorySlots; i++)
        {
            InventorySlots[i].SetActive(true);
            inventoryImages[i].enabled = true;
        }
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void AddItem(GameObject itemToAdd)
    {   if (itemToAdd.tag == "Flare")
        {
            InventorySlots[0].SetActive(true);
            inventoryImages[0].enabled = true;
        }
        else if (itemToAdd.tag == "Compass")
        {
            InventorySlots[1].SetActive(true);
            inventoryImages[1].enabled = true;
        }
        else if (itemToAdd.tag == "Flashlight")
        {
            InventorySlots[2].SetActive(true);
            inventoryImages[2].enabled = true;
        }
        else if(itemToAdd.tag == "Binoculars")
        {
            InventorySlots[3].SetActive(true);
            inventoryImages[3].enabled = true;
        }
        else if (itemToAdd.tag == "Flaregun")
        {
            InventorySlots[4].SetActive(true);
            inventoryImages[4].enabled = true;
        }
    }
	
    public void showInventory()
    {
        StartCoroutine(InventoryShown());

    }
    
    IEnumerator InventoryShown()
    {
        System.Console.WriteLine("Tab pressed");
        GetComponent<CanvasGroup>().alpha = 1;
        //for(int i = 0; i < numInventorySlots; i++)
        //{
        //    InventorySlots[i].SetActive(true);
        //    inventoryImages[i].enabled = true;
        //}

        yield return new WaitForSeconds(5);
        //yield return null;

        //for (int i = 0; i < numInventorySlots; i++)
        //{
            
        //    InventorySlots[i].SetActive(false);
        //    inventoryImages[i].enabled = false;
        //}

        float timer = 0f;

        while(timer < 1)
        {
            timer += Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, timer);
            yield return null;
        }

        yield break;
    }
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
