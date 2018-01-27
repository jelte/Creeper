using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerController pc = collider.gameObject.GetComponent<PlayerController> ();
		if (pc) {
			pc.climbable = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		PlayerController pc = collider.gameObject.GetComponent<PlayerController> ();
		if (pc) {
			pc.climbable = false;
		}
	}
}
