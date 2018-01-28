using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCanvasController : MonoBehaviour {

    public GameObject LR;
    public GameObject GU;
    public GameObject GD;
    public GameObject SU;
    public GameObject SD;

	private TraitManager traitManager;
	private Character character;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (traitManager == null && GameObject.FindGameObjectWithTag ("Player")) {
			traitManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<TraitManager> ();
			character = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
		}
		if (traitManager != null) {
			LR.SetActive (traitManager.isInvert && !character.Died());
			GD.SetActive (traitManager.isHeavy && !character.Died());
			GU.SetActive (traitManager.isBouncy && !character.Died());
			SU.SetActive(traitManager.isQuick && !character.Died());
			SD.SetActive(traitManager.isSlow && !character.Died());
        }
	}
}
