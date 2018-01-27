using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	GameObject character;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (character == null) {
			character = GameObject.FindGameObjectWithTag ("Player");
		} else {
			transform.position = Vector3.Lerp (
				transform.position, 
				character.transform.position + offset,
				Time.deltaTime
			);
		}
	}
}
