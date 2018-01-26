using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Character character;

	// Use this for initialization
	void Start () {
		character = GetComponent<Character> (); 
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Initialize movement
		Vector2 movement = Vector2.zero;
		// Add horizontal movement
		movement.x = Input.GetAxis ("Horizontal") * character.speed;

		// Add vertical velocity for jump
		if (Input.GetButtonDown ("Jump") && rb2d.velocity.y == 0) {
			rb2d.velocity += Physics2D.gravity * -1 * character.jumpModifier;
		}

		// Add movement to position based on deltaTime
		rb2d.position += movement * Time.deltaTime;
	}
}
