using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2Flag : MonoBehaviour
{
    private Rigidbody flagRb;

    // Start is called before the first frame update
    void Start()
    {
        // gets thr rigidbody of the flag
        flagRb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the flag collids with the ground it stops moving down
        if (collision.gameObject.tag == "Ground")
        {
            flagRb.isKinematic = true;
        }
    }
}
