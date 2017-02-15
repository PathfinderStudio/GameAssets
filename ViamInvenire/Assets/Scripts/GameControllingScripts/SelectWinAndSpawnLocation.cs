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
        index = randIndex.Next(spawnLocCont.transform.childCount);
        spawnLocCont.transform.GetChild(index);
        winLocCont.transform.GetChild(index);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
