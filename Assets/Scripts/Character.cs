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
	Rigidbody2D rb2d;
	TraitManager traitMan;
	public float gravityScale = 1.0f;


	// Use this for initialization
	void Start () {

		traitMan = GetComponent<TraitManager>();
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (playerTrait(10));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (traitMan.noTrait == false) {
				traitMan.noTrait = true;
				Debug.Log ("PAUSED TRAITS");
			}
			else if (traitMan.noTrait == true) {
				traitMan.noTrait = false;
				Debug.Log ("UNPAUSED TRAITS");
			}
		}
	}

	public void Move(Vector2 movement) {
		rb2d.position += movement * (traitMan.isInvert ? -1 : 1);
	}

	public void Attack() {
		// TODO: Do Attack.
	}

	// The Main PlayerTrait Thread
	public IEnumerator playerTrait(int delay)
	{
		// RESET TRAITS
		traitMan.isInvert = false;
		traitMan.isHeavy = false;
		traitMan.isBouncy = false;

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
			yield return new WaitForSeconds (5);
			traitMan.isInvert = true;
			SetGravityScale (1.0f);
			break;
			// CASE 1 is HEAVY (Increased Gravity)
		case 1:
			yield return new WaitForSeconds (5);
			traitMan.isHeavy = true;
			SetGravityScale (3.0f);
			break;
			/// CASE 2 is BOUNCY (Decreased Gravity)
		case 2:
			yield return new WaitForSeconds (5);
			traitMan.isBouncy = true;
			SetGravityScale (0.5f);
			break;
		}
		if (traitMan.noTrait == false) {
			yield return new WaitForSeconds (10);
			StartCoroutine (playerTrait (10));
		}
	}
		

	void SetGravityScale(float scale)
	{
		rb2d.gravityScale = 1f;
		gravityScale = 1f;
	}
}
