using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * Time.deltaTime * rotateSpeed, 0, Space.Self);
    }
}
