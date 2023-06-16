using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestablePlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 8f;

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
        Vector2 inp = move.ReadValue<Vector2>();
        movDir = new Vector2(inp.x, inp.y);

        Bounds();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movDir.x * speed, rb.velocity.y, movDir.y * speed);

        if (jump.IsPressed() && isGrounded)
        {
            isGrounded = false;
            rb.AddRelativeForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void Bounds()
    {
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
            isGrounded = true;
        }
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
    }
}
