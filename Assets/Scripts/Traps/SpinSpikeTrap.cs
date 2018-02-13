using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP;

public class SpinSpikeTrap : MonoBehaviour {

    private void Start()
    {
        //random z axis rotation
        transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //reduce player health
            collision.GetComponent<Character>().TakeDamage(1);
            Debug.Log("Player took damage");
        }
    }
}
