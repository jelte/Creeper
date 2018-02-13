using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour {

    public GameObject ObjectToMove;
    public Vector2 TriggerPosition;

    private void Start()
    {
        TriggerPosition = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for player
        if (collision.tag == "Player")
        {
            Debug.Log("Triggered!");
            ObjectToMove.GetComponent<DetectSpike>().tempPosition = collision.transform.position;
            ObjectToMove.GetComponent<DetectSpike>().MoveTowards = true;
            
        }

    }
}
