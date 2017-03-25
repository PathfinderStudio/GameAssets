﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWinAndSpawnLocation : MonoBehaviour
{
    [Header("DEBUG ONLY: Use this if you want to force buddy to spawn at a particular spawn location")]
    public Boolean forceCertainSpawn = false;
    [Header("DEBUG ONLY: Index of buddy's forced spawn. Zero based.")]
    public int forceSpawnIndex;

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

        if(forceCertainSpawn)
        {
            index = randIndex.Next(spawnLocCont.transform.childCount);
            spawnLocCont.transform.GetChild(index).gameObject.SetActive(false);
            winLocCont.transform.GetChild(index).gameObject.SetActive(false);
            forceSpawn();
        }
    }

    void forceSpawn()
    {
        index = forceSpawnIndex;
        spawnLocCont.transform.GetChild(index).gameObject.SetActive(true);
        winLocCont.transform.GetChild(index).gameObject.SetActive(true);
    }

}
