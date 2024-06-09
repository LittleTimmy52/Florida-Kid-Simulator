using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class TestController : MonoBehaviour, TestPlayerControls.IPlayerActions
{
    public float speed = 10f;

    private TestPlayerControls controls;
    private Vector3 inputVal = Vector3.zero;
    private NavMeshAgent agent;
    private Rigidbody rb;
    private CapsuleCollider cC;
    private bool isGrounded = true;
    private bool movGOrA = true;

    private void OnEnable()
    {
        controls = new TestPlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        cC = GetComponent<CapsuleCollider>();
        
        cC.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Update()
    {
        if (movGOrA == true)
        {
            GroundMove();
        }else
        {
            AirMove();
        }
    }

    private void GroundMove()
    {
        Vector3 newPos = transform.position + inputVal * Time.deltaTime * speed;
        agent.destination = newPos;
    }

    private void AirMove()
    {
        Vector3 newPos = transform.position + inputVal * Time.deltaTime * speed;
        transform.Translate(newPos);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input2D = context.ReadValue<Vector2>();
        inputVal.x = input2D.x;
        inputVal.z = input2D.y;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        print ("Looking");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
        print("jump function called. Grounded? " + isGrounded);
        if (isGrounded == true)
        {
            isGrounded = false;
            agent.destination = transform.position;
            agent.enabled = false;
            movGOrA = false;
            cC.enabled = true;
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.AddRelativeForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Ground")
        {
            if (isGrounded == false)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                cC.enabled = false;
                movGOrA = true;
                agent.enabled = true;
                agent.destination = transform.position;
                isGrounded = true;
            }
        }
    }
}
