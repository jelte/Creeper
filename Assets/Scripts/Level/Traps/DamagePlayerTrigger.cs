using ProjectFTP.Player;
using UnityEngine;

namespace ProjectFTP.Level.Traps
{
    public class DamagePlayerTrigger : MonoBehaviour
    {
        public int amount = 1;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
				collision.GetComponent<Character> ().TakeDamage (amount);
            }
        }
    }
}
