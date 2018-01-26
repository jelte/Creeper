using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D> ();
		if (rb) {
			rb.gravityScale = 0;
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D> ();
		if (rb) {
			rb.gravityScale = 1;
		}
	}
}
