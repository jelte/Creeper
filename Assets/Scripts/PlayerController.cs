using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#
using UnityEngine.SceneManagement;

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
    public bool climbable = false;

    // Use this for initialization
    void Start () {
		character = GetComponent<Character> (); 
		rb2d = GetComponent<Rigidbody2D> ();
        ani = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (character.playerHealth <= 0) {
			return;
        }

		Vector2 movement = Vector2.zero;
		movement.x = Input.GetAxis ("Horizontal") * character.speed;
		aniSpeed = movement.x;

		// Add Vertical movement
		if (climbable) {
			movement.y = Input.GetAxis ("Vertical") * character.climbSpeed;
		}
       
		character.Move (movement * Time.deltaTime);

		// Add vertical velocity for jump
		if (Input.GetButtonDown ("Jump") && jumping < (character.maxJumps-1)) {
			jumping += 1;
            ani.SetTrigger("Jump");
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


	void FixedUpdate()
	{
		// Check if any surface is climbable ( left , right, down & up )
		RaycastHit2D hitClimbableLeft = Physics2D.Raycast (transform.position, Vector2.left, 1f, LayerMask.GetMask ("Climbable"));
		RaycastHit2D hitClimbableRight = Physics2D.Raycast (transform.position, Vector2.right, 1f, LayerMask.GetMask ("Climbable"));
		RaycastHit2D hitClimbableUp = Physics2D.Raycast (transform.position, Vector2.up, 1f, LayerMask.GetMask ("Climbable"));
		RaycastHit2D hitClimbableDown = Physics2D.Raycast (transform.position, Vector2.down, 1f, LayerMask.GetMask ("Climbable"));

		if (hitClimbableLeft.collider != null || hitClimbableUp.collider != null || hitClimbableDown.collider != null || hitClimbableRight.collider != null) {
			climbable = true;
			rb2d.gravityScale = 0;
			rb2d.velocity = new Vector2 (rb2d.velocity.x, 0f);
		} else if (climbable) {
			// Stop climbing after 0.1 second, to be able to get over the top
			Invoke ("StopClimbing", 0.1f);
		}

		// Check if the character is grounded.
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 1f, LayerMask.GetMask ("Ground"));
		if (hit.collider != null || climbable) {
			jumping = 0;
            //Debug.Log("hit ground");
		}

		// Animation parameters
		ani.SetFloat("speed", aniSpeed);
        ani.SetFloat("velocity", rb2d.velocity.y);
        ani.SetBool("land", jumping == 0);
        ani.SetBool("faceRight", faceRight);
        ani.SetBool("attack", attackPressed);
	}

	void StopClimbing()
	{
		climbable = false;
		rb2d.gravityScale = character.gravityScale;
	}
}
