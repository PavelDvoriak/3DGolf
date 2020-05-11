using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = new Vector3(0f, 0f, -180f);

        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

}
