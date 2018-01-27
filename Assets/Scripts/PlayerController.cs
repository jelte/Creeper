using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlayerController : MonoBehaviour {

    //for movement
	Rigidbody2D rb2d;
	Character character;

    //for animation use
    Animator ani;
    float aniSpeed;
    bool attackPressed = false;
    bool canAttack = true;
    bool faceRight = true;
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
        if (aniSpeed > 0)
            faceRight = true;
        if (aniSpeed < 0)
            faceRight = false;
		if (Input.GetButtonDown ("Fire1")&&canAttack) {			
            StartCoroutine(Attack(0.5f));
		}
        Debug.Log(attackPressed);
    }

    IEnumerator Attack(float waitTime)
    {
        
        canAttack = false;
        attackPressed = true;
        character.Attack();
        yield return new WaitForSeconds(waitTime); ;
        attackPressed = false;
        canAttack = true;
    }

    private void FixedUpdate()
    {
        
        // ground check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            jumping = 0;
        }

        // animation set
        ani.SetFloat("speed", aniSpeed);
        ani.SetFloat("velocity", rb2d.velocity.y);
        ani.SetBool("land", jumping > 0);
        ani.SetBool("faceRight", faceRight);
        ani.SetBool("attack", attackPressed);
    }
}
