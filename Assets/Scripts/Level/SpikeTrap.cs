using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP;

namespace ProjectFTP.Level
{
    public class SpikeTrap : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                //reduce player health

                collision.GetComponent<Character>().TakeDamage(1);


                Debug.Log("Player took damage");

            }
        }
    }
}
