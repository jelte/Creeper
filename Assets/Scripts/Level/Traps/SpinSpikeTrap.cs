using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class SpinSpikeTrap : MonoBehaviour
    {
        public int damage = 1;

        void Start()
        {
            gameObject.AddComponent<DamagePlayerTrigger>().amount = damage;
            
            //random z axis rotation
            transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }
    }
}
