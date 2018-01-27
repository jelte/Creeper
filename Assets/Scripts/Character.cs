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

    public int PlayerHealth = 3;

	// Various Variables or references.
	Text invertWarning;
	Rigidbody2D rb2d;
	TraitManager traitMan;
	int countdown = 0;
	float startScale;
	public bool displayMessage = false;
	public float gravityScale = 1.0f;


	// Use this for initialization
	void Start () {

		traitMan = GetComponent<TraitManager>();
		rb2d = GetComponent<Rigidbody2D> ();

		// Get original Gravity Scale for reference.
		gravityScale = rb2d.gravityScale;
		//startScale = transform.localScale;
		// Start the endless barrage of traits loop. (With a delay provided of X)
		//StartCoroutine (playerTrait(10));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(Vector2 movement) {
		rb2d.position += movement * (traitMan.isInvert ? -1 : 1);
	}

	public void Attack() {
		// TODO: Do Attack.
	}

	// PICK UPS (Not needed if we do random-time trait deployment)
	public IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Pickup - Trait") )
		{
			countdown = 15;
			other.gameObject.SetActive (false);
			traitMan.isInvert = true;
			yield return new WaitForSeconds (15);
			traitMan.isInvert = false;
		}
	}

	// The Main PlayerTrait Thread
	public IEnumerator playerTrait(int delay)
	{
		while (true) 
		{
			// Deciding which trait to use.
			Random rand = new Random ();
			int traitChoice = Random.Range (0, 3);
			if (traitChoice == 0) {Debug.Log ("Current Trait = INVERTED ID: " + traitChoice);}
			if (traitChoice == 1) {Debug.Log ("Current Trait = HEAVY ID: " + traitChoice);}
			if (traitChoice == 2) {Debug.Log ("Current Trait = LIGHT ID: " + traitChoice);}
			switch (traitChoice) 
			{
			// Case 0 is INVERTED CONTROLS.
			case 0:
				displayMessage = true;
				yield return new WaitForSeconds (delay);
				traitMan.isInvert = true;
				SetGravityScale (1.0f);
				yield return new WaitForSeconds (delay);
				traitMan.isInvert = false;
				displayMessage = false;
				break;
				// CASE 1 is HEAVY (Increased Gravity)
			case 1:
				yield return new WaitForSeconds (delay);
				traitMan.isHeavy = true;
				SetGravityScale (3.0f);
				yield return new WaitForSeconds (delay);
				traitMan.isHeavy = false;
				break;
				/// CASE 2 is BOUNCY (Decreased Gravity)
			case 2:
				yield return new WaitForSeconds (delay);
				traitMan.isBouncy = true;
				SetGravityScale (0.5f);
				yield return new WaitForSeconds (delay);
				traitMan.isBouncy = false;
				break;
			case 3:
				yield return new WaitForSeconds (delay);
				traitMan.isShort = true;
				SetGravityScale (3.0f);
				yield return new WaitForSeconds (delay);
				traitMan.isShort = false;
				break;
			}
		}
	}

	void SetGravityScale(float scale)
	{
		rb2d.gravityScale = 1f;
		gravityScale = 1f;
	}
}
