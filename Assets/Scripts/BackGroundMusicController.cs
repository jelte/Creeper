﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP;

public class BackGroundMusicController : MonoBehaviour {

    private TraitManager traitManager;
    private Character character;
    private AudioSource bcmas;

    public GameManager gm;    

    // Use this for initialization
    void Start () {
        bcmas = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gm == null)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        if (GameObject.FindGameObjectWithTag("Player") && traitManager == null)
        {
            traitManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitManager>();
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }

        if (Time.timeScale == 0)
            bcmas.volume = 0f;
        else
            bcmas.volume = gm.backgroundVolume;        

        if (!character.IsAlive)
            bcmas.pitch = 0.3f;
        else if (traitManager.isInvert)
            bcmas.pitch = 1.5f;
        else
            bcmas.pitch = 1f;
    }
}
