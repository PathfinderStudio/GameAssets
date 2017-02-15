using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compassFollow : MonoBehaviour
{
    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = Quaternion.Euler(new Vector3(0f, camera.transform.rotation.eulerAngles.y + 90, 0f)); // rotates it too point north
    }
}
