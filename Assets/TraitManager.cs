﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitManager : MonoBehaviour 
{
	public bool isBlind = false;
	public bool isInvert = false;
	public bool isNarrow = false;
	public bool isBouncy = false;
	public bool isHeavy = false;
	public bool isMegaJump = false;
	public bool noTrait = false;

	//Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		//rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}
}
