using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicController : MonoBehaviour {

    private TraitManager traitManager;
    private PlayerController pc;
    private AudioSource bcmas;

    // Use this for initialization
    void Start () {
        bcmas = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (traitManager == null && GameObject.FindGameObjectWithTag("Player"))
        {
            traitManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitManager>();
        }
        if (pc == null && GameObject.FindGameObjectWithTag("Player"))
        {
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        if (pc.isPlayerDied)
            bcmas.pitch = 0.3f;
        else if (traitManager.isInvert)
            bcmas.pitch = 1.5f;
        else
            bcmas.pitch = 1f;
    }
}
