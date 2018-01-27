using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikeTrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            //reduce player health
            collision.GetComponent<Character>().PlayerHealth = -1;

            Debug.Log("Player took damage");

        }
    }

}
