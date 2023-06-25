using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestablePlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 8f;
    public float sensitivity = 5f;
    public TestGameManager gM;
    public Camera cam;

    private Rigidbody rb;
    private Vector2 movDir = Vector2.zero;
    private TestPlayerControls controler;
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private bool isGrounded;

    private void Awake()
    {
        controler = new TestPlayerControls();
    }

    private void OnEnable()
    {
        // Really all this is needed for is so the Input system doesent yell at me
        move = controler.Player.Move;
        look = controler.Player.Look;
        jump = controler.Player.Jump;

        move.Enable();
        look.Enable();
        jump.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Take input and move
        Vector2 inp = move.ReadValue<Vector2>();
        movDir = new Vector2(inp.x, inp.y);

        Bounds();

        Cam();
    }

    private void FixedUpdate()
    {
        // The actual movement
        rb.velocity = new Vector3(movDir.x * speed, rb.velocity.y, movDir.y * speed);

        //Jumping
        if (jump.IsPressed() && isGrounded)
        {
            isGrounded = false;
            rb.AddRelativeForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void Cam()
    {
        
    }

    private void Bounds()
    {
        // Keep player within the bounds of the level
        float boundBox = 50f;

        Vector3 pos = transform.position;

        if (pos.x > boundBox)
        {
            pos.x = boundBox;
        }else if (pos.x < -boundBox)
        {
            pos.x = -boundBox;
        }else if (pos.z > boundBox)
        {
            pos.z = boundBox;
        }else if (pos.z < -boundBox)
        {
            pos.z = -boundBox;
        }

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Ground")
        {
            // Landing
            isGrounded = true;
        }else if (collision.collider != null && collision.collider.tag == "Flag")
        {
            // Got flag, update count
            collision.gameObject.GetComponent<TestFlag>().ReLocate();

            gM.fCount++;

            // This logic needs to be here, dont worry
            if (gM.difficulty == 1)
            {
                gM.SpawnCops();
            }
            gM.SpawnCops();
        }
    }

    private void OnDisable()
    {
        // Again so the input system does not yell at me
        move.Disable();
        look.Disable();
        jump.Disable();
    }
}
