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
	float startGravityScale;
	public bool displayMessage = false;


	// Use this for initialization
	void Start () {

		traitMan = GetComponent<TraitManager>();
		rb2d = GetComponent<Rigidbody2D> ();

		// Get original Gravity Scale for reference.
		startGravityScale = rb2d.gravityScale;
		// Start the endless barrage of traits loop. (With a delay provided of X)
		StartCoroutine (playerTrait(10));
	}
	
	// Update is called once per frame
	void Update () {
		if (traitMan.isHeavy) {
			rb2d.gravityScale = 3f;
		} else {
			rb2d.gravityScale = startGravityScale;
		}
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
			int traitChoice = Random.Range (0, 2);
			Debug.Log (traitChoice);
			switch (traitChoice) 
			{
			// Case 0 is INVERTED CONTROLS.
			case 0:
				displayMessage = true;
				//StartCoroutine (ShowMessage ("Inverting Controls", 3));
				yield return new WaitForSeconds (delay);
				//StartCoroutine (ShowMessage ("Controls Inverted", delay));
				traitMan.isInvert = true;
				yield return new WaitForSeconds (delay);
				traitMan.isInvert = false;
				displayMessage = false;
				break;
				// CASE 1 is HEAVY (Increased Gravity)
			case 1:
				yield return new WaitForSeconds (delay);
				traitMan.isHeavy = true;
				yield return new WaitForSeconds (delay);
				traitMan.isHeavy = false;
				break;
			}
		}
	}
}
