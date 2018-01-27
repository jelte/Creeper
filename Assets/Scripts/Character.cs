using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	[Range(1.0f, 10.0f)]
	public float speed = 5f;
	[Range(0.1f, 2.0f)]
	public float jumpModifier = 0.5f;
	[Range(0.1f, 2.0f)]
	public float climbSpeed = 0.25f;

	public int maxJumps = 2;

	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(Vector2 movement) {
		rb2d.position += movement;
	}

	public void Attack() {
		// TODO: Do Attack.
	}
}
