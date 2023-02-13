using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    #region PRIVATE

    private PhotonView view;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isPaused = false;
    private float speed = 300f;
    private float jumpForce = 500f;
    private float boundX = 50f;
    private float boundZ = 50f;

    #endregion

    #region PUBLIC


    public GameObject[] cameras;
    public int cameraIndex = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        if (view.IsMine)
        {
            Cursor.visible = false;
            cameras[cameraIndex].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (!isPaused)
            {
                Bounds();
                CycleCam();
            }

            Pause();
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (!isPaused)
            {
                Move();
                Jump();
            }
        }
    }

    private void Bounds()
    {
        Vector3 tmp = transform.position;
        if (tmp.x > boundX) { tmp.x = boundX; }
        if (tmp.x < -boundX) { tmp.x = -boundX; }
        if (tmp.z > boundX) { tmp.z = boundZ; }
        if (tmp.z < -boundX) { tmp.z = -boundZ; }
        transform.position = tmp;
    }

    #region INPUT HANDLERS

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 move = new Vector3(horizontal, 0, vertical) * speed * Time.fixedDeltaTime;
            move.y = rb.velocity.y;
            rb.velocity = transform.TransformDirection(move);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce);

                isGrounded = false;
            }
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;

            if (!isPaused)
            {
                Cursor.visible = false;

                cameras[cameraIndex].GetComponent<CameraController>().enabled = true;
            }
            else
            {
                Cursor.visible = true;

                cameras[cameraIndex].GetComponent<CameraController>().enabled = false;
            }
        }
    }

    private void CycleCam()
    {
        if (view.IsMine)
        {
            if (Input.GetButtonDown("F5"))
            {
                cameras[cameraIndex].SetActive(false);
                cameraIndex++;
                cameraIndex %= 3;
                cameras[cameraIndex].SetActive(true);
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
}
