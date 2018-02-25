using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectFTP.Level.Traps
{
    public class DetectTrigger : MonoBehaviour
    {

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
                ObjectToMove.GetComponent<MovignSpike>().TargetReference = collision.gameObject;
                ObjectToMove.GetComponent<MovignSpike>().MoveTowards = true;

            }

        }
    }
}