using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrokeAngleIndicator : MonoBehaviour
{

    BallControll strokeManager;

    // Start is called before the first frame update
    void Start()
    {
        strokeManager = GameObject.FindObjectOfType<BallControll>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = strokeManager.PlayerBall.position;
        this.transform.rotation = Quaternion.Euler(0, strokeManager.StrokeAngle, 0);
    }
}
