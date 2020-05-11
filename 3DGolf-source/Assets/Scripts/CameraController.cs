using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;

    private Vector3 offset;


    // Update is called once per frame
    void FixedUpdate()
    {
        

        Vector3 positionToGo = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionToGo, speed * Time.deltaTime);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }

    public void SetOffset()
    {
        offset = transform.position - target.position;
    }
}
