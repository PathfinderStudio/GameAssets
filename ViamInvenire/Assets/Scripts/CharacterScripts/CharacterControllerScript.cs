using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public double runSpeed { get; set; }
 

    private double _runSpeed;
    private Rigidbody player;
    private Vector3 playerMovement;
    private bool jumping;
    private float jumpAmount;
    private bool canMove;
    private float maxVelocity;
    private Vector3 velPlayer;

    // Use this for initialization
    void Start()
    {
        runSpeed = 100.0;
        _runSpeed = runSpeed;
        player = this.GetComponent<Rigidbody>();
        playerMovement = Vector3.zero;
        jumping = false;
        jumpAmount = 500;
        maxVelocity = 10.0f;
        canMove = true;
        velPlayer = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                maxVelocity = 20.0f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                maxVelocity = 10.0f;
            }
            
            playerMovement.x = Input.GetAxis("Horizontal");
            playerMovement.z = Input.GetAxis("Vertical");
            
            if (playerMovement.x == 0 && playerMovement.z == 0)
            {
                velPlayer = player.velocity * 0.9f;
            }
            else
            {
                velPlayer = (playerMovement.z * transform.forward + playerMovement.x * transform.right) * (float)_runSpeed;
            }
            
            if(canMove)
            {
                player.velocity = velPlayer;  //prevent movement left to right and forward back, while off the ground, use raycast to find distance to ground?
            }
            
            if (player.velocity.x > maxVelocity)
            {
                player.velocity = new Vector3(maxVelocity, player.velocity.y, player.velocity.z);
            }
            if (player.velocity.z > maxVelocity)
            {
                player.velocity = new Vector3(player.velocity.x, player.velocity.y, maxVelocity);
            }
            if (player.velocity.x < -maxVelocity)
            {
                player.velocity = new Vector3(-maxVelocity, player.velocity.y, player.velocity.z);
            }
            if (player.velocity.z < -maxVelocity)
            {
                player.velocity = new Vector3(player.velocity.x, player.velocity.y, -maxVelocity);
            }
            
            //player.transform.Translate(playerMovement.x * (float)_runSpeed * Time.deltaTime, 0, playerMovement.z * (float)_runSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                jumping = true;
                player.AddForce(Vector3.up * jumpAmount);
            }
            if (player.velocity.y == 0)
            {
                jumping = false;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Ground")
        {
            //Debug.Log("On Ground");
            canMove = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if(col.transform.tag == "Ground")
        {
            //Debug.Log("Not on ground");
            canMove = false;
        }
    }
}
