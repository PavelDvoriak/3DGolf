using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSettings : MonoBehaviour
{
    private Rigidbody ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = Mathf.Infinity;
        ball.sleepThreshold = 0.5f;
    }
}
