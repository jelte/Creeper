using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP;

namespace ProjectFTP.Level
{
    public class MovignSpike : MonoBehaviour
    {


        public bool MoveTowards = false;
        public GameObject TargetReference;
        private Vector3 tempPosition;
        public float moveSpeed;


        private void Start()
        {
            tempPosition = TargetReference.transform.position;
            moveSpeed = 5.0f;
        }


        private void Update()
        {
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


        private void OnTriggerEnter2D(Collider2D collision)
        {
            //on trigger hit kill player
            if (collision.CompareTag("Player"))
            {
                //reduce player health
                collision.GetComponent<Character>().TakeDamage(1);
            }
        }
    }
}
