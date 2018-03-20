using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class LimitBallVelocity : MonoBehaviour
    {
        private Rigidbody2D rg;
        public float maxballSpeed = 20.0f;

        void Start()
        {
            rg = this.GetComponent<Rigidbody2D>();
        }
        
        void FixedUpdate()
        {
            if (Mathf.Abs(rg.velocity.x) > maxballSpeed || Mathf.Abs(rg.velocity.y) > maxballSpeed)
            {
                // clamp velocity:
                Vector3 newVelocity = rg.velocity.normalized;
                newVelocity *= maxballSpeed;
                rg.velocity = newVelocity;
            }
        }
    }
}
