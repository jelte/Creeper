using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb2d;
	Character character;

    Animator ani;
    float aniSpeed;
    bool land = true;

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
        if (Input.GetButtonDown ("Jump") && Mathf.Abs(rb2d.velocity.y) < 0.01f) {
			rb2d.velocity += Physics2D.gravity * -1f * character.jumpModifier;
           
        }

		// Trigger Attack
		if (Input.GetButtonDown ("Fire1")) {
			character.Attack ();
		}

        if (rb2d.velocity.y == 0)
            land = true;
        else
            land = false;
    }

    private void FixedUpdate()
    {
        ani.SetFloat("speed", aniSpeed);
        ani.SetFloat("velocity", rb2d.velocity.y);
        ani.SetBool("land", land);
    }



}
