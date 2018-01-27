using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCanvasController : MonoBehaviour {

    public GameObject LR;
    public GameObject GU;
    public GameObject GD;

	private TraitManager traitManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (traitManager == null && GameObject.FindGameObjectWithTag ("Player")) {
			traitManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<TraitManager> ();
		}
		if (traitManager != null) {
			LR.SetActive (traitManager.isInvert);
			GU.SetActive (traitManager.isHeavy);
			GD.SetActive (traitManager.isBouncy);
		}
	}
}
