using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

	[Range(1.0f, 10.0f)]
	public float speed = 5f;
	[Range(0.1f, 2.0f)]
	public float jumpModifier = 0.5f;
	[Range(1.0f, 10.0f)]
	public float climbSpeed = 5f;

	public int maxJumps = 2;

	public int playerHealth = 3;

	// Various Variables or references.
	Rigidbody2D rb2d;
	TraitManager traitMan;
	Animator anim;
	Camera cam;
	public float gravityScale = 1.0f;

    // Use this for initialization
    void Start()
    {

        traitMan = GetComponent<TraitManager>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        gravityScale = rb2d.gravityScale;
        StartCoroutine(playerTrait(10));
    }

    public void Move(Vector2 movement) {
		rb2d.position += movement * (traitMan.isInvert ? -1 : 1);
	}

	public void Attack() {
		// TODO: Do Attack.
	}

	public bool Died() {
		return playerHealth <= 0;
	}

	// The Main PlayerTrait Thread
	public IEnumerator playerTrait(int delay)
	{
		// RESET TRAITS
		traitMan.isInvert = false;
		traitMan.isHeavy = false;
		traitMan.isBouncy = false;
		traitMan.isZoomedIn = false;
		traitMan.isZoomedOut = false;
		traitMan.isMegaJumps = false;
		traitMan.isNoJump = false;
		traitMan.isSlow = false;
		traitMan.isQuick = false;
		SetCameraOrthSize (7);
		SetJumpLimit (2);
		SetPlayerSpeed (5f);

		// Deciding which trait to use.
		Random rand = new Random ();
		int traitChoice = Random.Range (0, 7);
		if (traitChoice == 0) {Debug.Log ("Current Trait = INVERTED ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 1) {Debug.Log ("Current Trait = HEAVY ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 2) {Debug.Log ("Current Trait = LIGHT ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 3) {Debug.Log ("Current Trait = ZOOMED OUT ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 4) {Debug.Log ("Current Trait = ZOOMED IN ID: " + traitChoice + " TIME UNTIL: " + delay);}
		//if (traitChoice == 5) {Debug.Log ("Current Trait = MEGA JUMPS ID: " + traitChoice + " TIME UNTIL: " + delay);}
		//if (traitChoice == 6) {Debug.Log ("Current Trait = NO JUMPS ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 5) {Debug.Log ("Current Trait = QUICK ID: " + traitChoice + " TIME UNTIL: " + delay);}
		if (traitChoice == 6) {Debug.Log ("Current Trait = SLOW ID: " + traitChoice + " TIME UNTIL: " + delay);}
			
		switch (traitChoice) 
		{
		// Case 0 is INVERTED CONTROLS.
		case 0:
			yield return new WaitForSeconds (delay);
			traitMan.isInvert = true;
			Debug.Log ("CURRENTLY INVERTED");
			SetGravityScale (1.0f);
			break;
			// CASE 1 is HEAVY (Increased Gravity)
		case 1:
			yield return new WaitForSeconds (delay);
			traitMan.isHeavy = true;
			Debug.Log ("CURRENTLY HEAVY");
			SetGravityScale (3.0f);
			break;
			/// CASE 2 is BOUNCY (Decreased Gravity)

		case 2:
			yield return new WaitForSeconds (delay);
			traitMan.isBouncy = true;
			Debug.Log ("CURRENTLY LIGHT");
			SetGravityScale (0.5f);
			break;
		case 3:
			yield return new WaitForSeconds (delay);
			traitMan.isZoomedOut = true;
			Debug.Log ("CURRENTLY ZOOMED OUT");
			SetCameraOrthSize (10);
			break;
		case 4:
			yield return new WaitForSeconds (delay);
			traitMan.isZoomedIn = true;
			Debug.Log ("CURRENTLY ZOOMED IN");
			SetCameraOrthSize (3);
			break;
		/*case 5:
			yield return new WaitForSeconds (delay);
			traitMan.isMegaJumps = true;
			Debug.Log ("CURRENTLY: MEGA JUMPING");
			SetJumpLimit (5);
			break;
		case 6:
			yield return new WaitForSeconds (delay);
			traitMan.isNoJump = true;
			Debug.Log ("CURRENTLY: NO JUMPING");
			SetJumpLimit (0);
			break;*/
		case 5:
			yield return new WaitForSeconds (delay);
			traitMan.isQuick = true;
			Debug.Log ("CURRENTLY: QUICK");
			SetPlayerSpeed (8f);
			break;
		case 6:
			yield return new WaitForSeconds (delay);
			traitMan.isSlow = true;
			Debug.Log ("CURRENTLY: SLOW");
			SetPlayerSpeed (2.5f);
			break;
		}
		if (traitMan.noTrait == false && !Died()) {
			yield return new WaitForSeconds (10);
			StartCoroutine (playerTrait (10));
		}
	}
		

	void SetGravityScale(float scale)
	{
		rb2d.gravityScale = 1f;
		gravityScale = 1f;
	}

	void SetCameraOrthSize(float size)
	{
		cam.orthographicSize = size;
	}
	void SetJumpLimit(int limit)
	{
		maxJumps = limit;
	}
	void SetPlayerSpeed(float newSpeed)
	{
		speed = newSpeed;
	}
}
