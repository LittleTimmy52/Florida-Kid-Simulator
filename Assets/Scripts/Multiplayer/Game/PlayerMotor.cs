using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isPaused = false;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    // recieve inputs from InputManager and apply to charictor controller
    public void ProcessMove (Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        controller.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            print("paused");
            Cursor.visible = true;
        }
        else
        {
            print("unpaused");
            Cursor.visible = false;
        }
    }
}
