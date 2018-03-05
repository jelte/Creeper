using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class FireWall : MonoBehaviour
    {
        public GameObject BallToSpawn;
        public float delay = 1;
        public float repeatRate = 2;
        public float speed = 1000.0f;
        public float maxDistance = 200.0f;
        public int amount = 2;

        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            InvokeRepeating("Fire", delay, repeatRate);
        }

        void Fire()
        {
            float offSetDirection = Random.Range(0.0f, 360.0f);

            if (player != null)
            {
                float distance = (player.transform.position - transform.position).magnitude;
                if (distance <= maxDistance)
                {
                    // HACK: Disable box collider
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position, distance);
                    // HACK: Enable box collider
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    if (hit.collider == null)
                    {
                        offSetDirection = Vector2.Angle(transform.position, player.transform.position) / Mathf.Rad2Deg;
                    }
                }
            }

            //fire here
            float angle = 360 / amount;
            for (float arc = 0; arc < 360; arc += angle)
            {
                Vector2 direction = GetDirection(offSetDirection + arc);
                Instantiate(BallToSpawn, transform.position + (Vector3)direction, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(direction.normalized * speed);
            }
        }

        Vector2 GetDirection(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }
}