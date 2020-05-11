using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollider : MonoBehaviour
{
    public bool BallInHole { get; protected set; }
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        BallInHole = false;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Its there");
            BallInHole = true;
            gameManager.EndTurn();
            Debug.Log(BallInHole);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BallInHole = false;
        }
    }

    private void OnEnable()
    {
        BallInHole = false;
    }

}
