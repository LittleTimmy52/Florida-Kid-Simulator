using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlag : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ReLocate();
    }

    public void ReLocate()
    {
        float boundBox = 49f;

        // Position flag
        transform.position = new Vector3(Random.Range(-boundBox, boundBox), 100, Random.Range(-boundBox, boundBox));

        // Unfreeze the Rigidbody but refreeze rotation
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Ground")
        {
            // Freeze the Rigidbody position when it hits the ground
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
