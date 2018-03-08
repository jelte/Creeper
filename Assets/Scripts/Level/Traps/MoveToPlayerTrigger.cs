using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class MoveToPlayerTrigger : MonoBehaviour
    {
        public GameObject ObjectToMove;

        void OnTriggerEnter2D(Collider2D collision)
        {
            //check for player
            if (collision.tag == "Player")
            {
                ObjectToMove.GetComponent<MovingSpike>().MoveTowards = true;
            }
        }
    }
}