using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
		if(collision.CompareTag("Player")) {
            //reduce player health
            collision.GetComponent<Character>().PlayerHealth =- 1;
            //push player away from spike
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);

            Debug.Log("Player took damage");

        }
    }
}
