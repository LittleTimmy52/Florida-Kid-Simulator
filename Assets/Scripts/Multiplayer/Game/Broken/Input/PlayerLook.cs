using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRot = 0f;
    private PhotonView view;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void ProcessLook(Vector2 input)
    {
        if (view.IsMine)
        {
            float mouseX = input.x;
            float mouseY = input.y;

            // calc cam rot for up/down
            xRot -= (mouseY * Time.deltaTime) * ySensitivity;
            xRot = Mathf.Clamp(xRot, -80f, 80f);

            // apply to cam
            cam.transform.localRotation = Quaternion.Euler(xRot, 0, 0);

            // rot player for left/right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
    }
}
