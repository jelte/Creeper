using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	[Range(1.0f, 10.0f)]
	public float speed = 5f;
	[Range(0.1f, 2.0f)]
	public float jumpModifier = 0.5f;
	[Range(0.1f, 1.0f)]
	public float climbModifier = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
