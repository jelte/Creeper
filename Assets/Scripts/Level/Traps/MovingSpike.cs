using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class MovingSpike : MonoBehaviour
    {
        public int damage = 1;
        public bool MoveTowards = false;
        public GameObject TargetReference;
        private Vector3 tempPosition;
        public float moveSpeed = 5.0f;

        void Start()
        {
            gameObject.AddComponent<DamagePlayerTrigger>().amount = damage;
            tempPosition = TargetReference.transform.position;
        }

        void Update()
        {
            //move object to player
            if (MoveTowards == true)
            {
                //move
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, tempPosition, step);

                //move towards false once there
                if (gameObject.transform.position == tempPosition)
                {
                    MoveTowards = false;
                }
            }
        }
    }
}