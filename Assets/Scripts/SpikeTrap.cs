using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
		if(collision.CompareTag("Player")) {
            //reduce player health

            collision.GetComponent<Character>().playerHealth =- 1;
            

            Debug.Log("Player took damage");

        }
    }
}
