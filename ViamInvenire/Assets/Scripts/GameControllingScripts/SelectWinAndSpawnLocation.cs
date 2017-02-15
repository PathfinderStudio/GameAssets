using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWinAndSpawnLocation : MonoBehaviour
{
    [Header("Spawn Location Container")]
    public GameObject spawnLocCont;

    [Header("Win Location Container")]
    public GameObject winLocCont;

    //Use a random number to pick where the player should initially spawn.
    private System.Random randIndex;
    private int index;
    // Use this for initialization
    void Start()
    {
        randIndex = new System.Random();
        randIndex.Next();
        for(int i = 0; i < spawnLocCont.transform.childCount; i++)
        {
            spawnLocCont.transform.GetChild(i).gameObject.SetActive(false);
            winLocCont.transform.GetChild(i).gameObject.SetActive(false);
        }
        index = randIndex.Next(spawnLocCont.transform.childCount);
        spawnLocCont.transform.GetChild(index).gameObject.SetActive(true);
        winLocCont.transform.GetChild(index).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
