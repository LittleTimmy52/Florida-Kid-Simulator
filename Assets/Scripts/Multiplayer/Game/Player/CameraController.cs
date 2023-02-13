using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    private float xRot = 0f;
    private int camIndex;

    public GameObject parent;
    public float sensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (parent.GetComponent<PhotonView>().IsMine)
        {
            Look();

            camIndex = parent.GetComponent<PlayerController>().cameraIndex;
        }
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // calc cam rot for up/down
        xRot -= (mouseY * Time.deltaTime) * sensitivity;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        // apply to cam
        Quaternion camRot = Quaternion.Euler(xRot, 0, 0);

        if (camIndex == 1)
        {
            camRot = Quaternion.Euler(-xRot, 0, 0);
        }

        transform.localRotation = camRot;

        // rot player for left/right
        parent.transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensitivity);
    }
}
