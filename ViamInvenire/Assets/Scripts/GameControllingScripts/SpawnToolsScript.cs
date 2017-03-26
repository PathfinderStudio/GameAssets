using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToolsScript : MonoBehaviour
{
    //public List<GameObject> stuff;
    [Header("Tools To Spawn")]
    public GameObject compass;
    public GameObject flashlight;
    public GameObject binos;
    public GameObject emergencyFlare;

    public int numberOfToolsToSpawn = 6;

	// Use this for initialization
	void Start ()
    {
        //compass = GameObject.FindGameObjectWithTag("Compass");
        //flashlight = GameObject.FindGameObjectWithTag("Flashlight");
        //binos = GameObject.FindGameObjectWithTag("Binoculars");
        //emergencyFlare = GameObject.FindGameObjectWithTag("Flare");
	}

    public void BeginSpawning()
    {
        for(int i = 0; i < numberOfToolsToSpawn; i++)
        {
            if(i < 3)
            {
                if(i == 0)
                {
                    //compass.transform.position = this.transform.GetChild(i).transform.position;
                    //compass.transform.rotation = this.transform.GetChild(i).transform.rotation;
                    //compass.transform.parent = this.transform.GetChild(i).transform.parent;
                    Instantiate(compass, this.transform.GetChild(i).transform.position, this.transform.GetChild(i).transform.rotation, this.transform.GetChild(i).transform);
                }
                else if(i == 1)
                {
                    //flashlight.transform.position = this.transform.GetChild(i).transform.position;
                    //flashlight.transform.rotation = this.transform.GetChild(i).transform.rotation;
                    //.transform.parent = this.transform.GetChild(i).transform.parent;
                    Instantiate(flashlight, this.transform.GetChild(i).transform.position, this.transform.GetChild(i).transform.rotation, this.transform.GetChild(i).transform);
                }
                else if(i == 2)
                {
                    //binos.transform.position = this.transform.GetChild(i).transform.position;
                    //binos.transform.rotation = this.transform.GetChild(i).transform.rotation;
                    //binos.transform.parent = this.transform.GetChild(i).transform.parent;
                    Instantiate(binos, this.transform.GetChild(i).transform.position, this.transform.GetChild(i).transform.rotation, this.transform.GetChild(i).transform);
                }
            }
            else
            {
                Instantiate(emergencyFlare, this.transform.GetChild(i).transform.position, this.transform.GetChild(i).transform.rotation, this.transform.GetChild(i).transform);
            }
        }
    }
}
