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
    private bool holdingJump;
    private bool grounded;
    private float jumpHoldTime;
    private float frictionForce;
    

    // Use this for initialization
    void Start()
    {
        runSpeed = 100.0;
        _runSpeed = runSpeed;
        player = this.GetComponent<Rigidbody>();
        playerMovement = Vector3.zero;
        jumping = false;
        jumpAmount = 10;
        maxVelocity = 10.0f;
        canMove = true;
        velPlayer = Vector3.zero;
        holdingJump = false;
        grounded = true;
        jumpHoldTime = 0.0f;
        frictionForce = 2.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (grounded)
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
            }
            else
            {
                playerMovement.x = playerMovement.x * 0.9f;
                playerMovement.z = playerMovement.z * 0.9f;
                //velPlayer = new Vector3(player.velocity.x * 0.5f, player.velocity.y, player.velocity.z * 0.5f);
            }
            if (playerMovement.x == 0 && playerMovement.z == 0)
            {
                
                playerMovement.x = playerMovement.x * 0.9f;
                playerMovement.z = playerMovement.z * 0.9f;
                velPlayer = new Vector3(playerMovement.x, player.velocity.y, playerMovement.z);
            }
            else
            {
                velPlayer = (playerMovement.z * transform.forward + playerMovement.x * transform.right) * (float)_runSpeed;
                velPlayer.y = player.velocity.y;
            }

            //add movement to the velocity of the character
            if (canMove)
            {
                player.velocity = velPlayer;  //prevent movement left to right and forward back, while off the ground, use raycast to find distance to ground?
            }

            jumpingMethod();
            IsGrounded();

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
            
        }
    }

    private void jumpingMethod()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping && grounded)
        {
            jumping = true;
            player.velocity += (Vector3.up * jumpAmount);
            grounded = false;
        }
        else if (Input.GetKey(KeyCode.Space) && jumpHoldTime < 0.1f)
        {
            jumpHoldTime += Time.deltaTime;
            player.velocity += (Vector3.up * jumpAmount);
            holdingJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            holdingJump = false;
            
        }
        if (grounded)
        {
            jumpHoldTime = 0.0f;
            jumping = false;
        }
    }

    private void IsGrounded()
    {
        int mask = 1;
        int layerMask = mask << 8; //ground is on 8th
        float length = 2.5f;

        Vector3 rayPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + this.transform.localScale.y / 2, this.transform.localPosition.z);
        if (Physics.Raycast(rayPos, Vector3.down, length, layerMask))
        {
            grounded = true;
            //Debug.Log("on ground");
        }
        else
        {
            grounded = false;
            player.velocity += new Vector3(0, -10f, 0);
            if(player.velocity.y < -50)
            {
                player.velocity = new Vector3(player.velocity.x, -90, player.velocity.z);
            }
        }
    }

    /*
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
    */
}
