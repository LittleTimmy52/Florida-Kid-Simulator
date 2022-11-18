using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PRIVATE

    private float horizontalInput;
    private float verticalInput;
    private float speed = 10f;
    private float xBound = 50f;
    private float zBound = 50f;
    private float jumpForce = 500f;
    private bool isOnGround;
    private Rigidbody playerRb;

    #endregion

    #region  PUBLIC



    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // sets the rigidbody
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region  INPUTS AND MOVEMENT

        // updates val of the input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // fixes collision due to speed
        if (horizontalInput != 0 && verticalInput != 0)
        {
            speed = 7f;
        }
        else
        {
            speed = 10f;
        }

        // moves the player
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        // jumping
        if (Input.GetButtonDown("Jump") && isOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce);
            isOnGround = false;
        }

        #endregion

        #region BOUNDS

        // the bounds on the x axis
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        // the bounds on the y axis
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }

        #endregion

        if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 2.4f, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player lands on the ground it sets isOnGround to true
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }
}
