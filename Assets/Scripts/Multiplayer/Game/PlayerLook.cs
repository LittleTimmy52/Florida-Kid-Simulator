using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRot = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
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
