using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillUnderWater : MonoBehaviour
{
    private CharacterDeathScript toKill;

    // Use this for initialization
    void Start()
    {
        toKill = this.GetComponent<CharacterDeathScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -160)
        {
            toKill.KillPlayer();
        }
    }
}
