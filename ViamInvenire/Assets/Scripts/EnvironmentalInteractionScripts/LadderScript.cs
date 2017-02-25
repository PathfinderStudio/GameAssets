using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (Time.timeScale != 0)
        {
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("ON Ladder");
                col.gameObject.SendMessage("Climbing", true, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(Time.timeScale != 0)
        {
            if(col.gameObject.tag == "Player")
            {
                Debug.Log("Off Ladder");
                col.gameObject.SendMessage("Climbing", false, SendMessageOptions.DontRequireReceiver);
            }
        }
        
    }
}
