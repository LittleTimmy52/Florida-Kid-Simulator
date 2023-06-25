using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2Rotation : MonoBehaviour
{
    public GameObject objToRotate;
    public float rotateSpeed = -20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objToRotate.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
    }
}
