using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Character character;

    Animator ani;
    float aniSpeed;
    int jumping = 0;

    // Use this for initialization
    void Start () {
		character = GetComponent<Character> (); 
		rb2d = GetComponent<Rigidbody2D> ();
        ani = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		// Initialize movement
		Vector2 movement = Vector2.zero;
		// Add horizontal movement
		movement.x = Input.GetAxis ("Horizontal") * character.speed;
        aniSpeed = movement.x;
        // Add Vertical movement
        if (rb2d.gravityScale == 0) {
			movement.y = Input.GetAxis ("Vertical") * character.climbSpeed;            
        }
        
		character.Move (movement * Time.deltaTime);

		// Add vertical velocity for jump
		if (Input.GetButtonDown ("Jump") && jumping < (character.maxJumps-1)) {
			jumping += 1;
			rb2d.velocity += Physics2D.gravity * -1f * (character.jumpModifier/jumping);
		}

		// Trigger Attack
		if (Input.GetButtonDown ("Fire1")) {
			character.Attack ();
		}
    }

    private void FixedUpdate()
    {
  
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            jumping = 0;
        }

        ani.SetFloat("speed", aniSpeed);
        ani.SetFloat("velocity", rb2d.velocity.y);
        ani.SetBool("land", jumping > 0);
    }
}
