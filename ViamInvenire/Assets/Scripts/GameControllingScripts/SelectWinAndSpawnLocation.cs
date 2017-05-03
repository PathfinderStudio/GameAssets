using System;
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

    [Header("Container For Tool Spawn Locations")]
    public GameObject toolsSpawnLoc;

    //Use a random number to pick where the player should initially spawn.
    private System.Random randIndex;
    private int index;

    // Use this for initialization
    void Start()
    {

        Debug.Log("This is the Start Method of SelectWinAndSpawnLocation");
        randIndex = new System.Random();
        randIndex.Next();
        for (int i = 0; i < spawnLocCont.transform.childCount; i++)
        {
            spawnLocCont.transform.GetChild(i).gameObject.SetActive(false);
            winLocCont.transform.GetChild(i).gameObject.SetActive(false);
            toolsSpawnLoc.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (forceCertainSpawn)
        {
            forceSpawn();
        }
        else
        {
            index = randIndex.Next(spawnLocCont.transform.childCount);
            spawnLocCont.transform.GetChild(index).gameObject.SetActive(true);
            winLocCont.transform.GetChild(index).gameObject.SetActive(true);

            toolsSpawnLoc.transform.GetChild(index).gameObject.SetActive(true);
            toolsSpawnLoc.transform.GetChild(index).gameObject.GetComponent<SpawnToolsScript>().BeginSpawning();
        }
                
    }

    /// <summary>
    /// Ignores random index chosen for the spawn and win locations and sets it to 
    /// value determined by public index variable.
    /// </summary>
    void forceSpawn()
    {
        index = forceSpawnIndex;
        spawnLocCont.transform.GetChild(index).gameObject.SetActive(true);
        winLocCont.transform.GetChild(index).gameObject.SetActive(true);
        toolsSpawnLoc.transform.GetChild(index).gameObject.SetActive(true);
        toolsSpawnLoc.transform.GetChild(index).gameObject.GetComponent<SpawnToolsScript>().BeginSpawning();
    }
    public int getSpawnIndex()
    {
        return index;
    }

}
