using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountOfFlares : MonoBehaviour {

    public int amount = 3; 

    public void IncrementAmount()
    {
        amount++;
        Debug.Log("Gained Flares");
        gameObject.GetComponent<Text>().text = amount.ToString();
    }

    public void DecrementAmount()
    {
        amount--;
        Debug.Log("Lost Flares");
        gameObject.GetComponent<Text>().text = amount.ToString();
    }

    public void SetAmount()
    {
        //amount = 3;
        gameObject.GetComponent<Text>().text = amount.ToString();
    }
}
