using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	GameObject character;
	public Vector3 offset;
	public float maxDistance = 10f;

	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (character == null) {
			character = GameObject.FindGameObjectWithTag ("Player");
		} else {
			float magnitude = (transform.position - (character.transform.position + offset)).magnitude/10f;
			transform.position = Vector3.Lerp (
				transform.position, 
				character.transform.position + offset,
				Mathf.Clamp(magnitude, 0f, 1f)
			);
		}
	}
}
