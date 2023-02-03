using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    #region PRIVATE

    private PhotonView view;
    private Rigidbody rb;
    private float xRot = 0f;
    private bool isGrounded;
    private bool isPaused = false;

    #endregion

    #region PUBLIC

    public float speed = 300f;
    public float jumpForce = 500f;

    [Header("Camera")]
    public Camera cam;
    public float xSensitivity = 100f;
    public float ySensitivity = 100f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        if (view.IsMine)
        {
            Cursor.visible = false;
            cam.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Look();
            Pause();
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
            Jump();
        }
    }

    #region INPUT HANDLERS

    public void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 move = new Vector3(horizontal, 0, vertical) * speed * Time.fixedDeltaTime;
            move.y = rb.velocity.y;
            rb.velocity = transform.TransformDirection(move);
            print(rb.velocity);
            print(isGrounded);

        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    public void Look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // calc cam rot for up/down
        xRot -= (mouseY * Time.deltaTime) * ySensitivity;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        // apply to cam
        cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);

        // rot player for left/right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce);

                isGrounded = false;
            }
        }
    }

    public void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;

            if (!isPaused)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
