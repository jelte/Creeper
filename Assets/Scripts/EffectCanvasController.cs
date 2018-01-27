using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCanvasController : MonoBehaviour {

    public bool isSwitchedLeftandRight;
    public GameObject LR;
    public bool isGravityUp;
    public GameObject GU;
    public bool isGravityDown;
    public GameObject GD;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LR.SetActive(isSwitchedLeftandRight);
        GU.SetActive(isGravityUp);
        GD.SetActive(isGravityDown);		
	}
}
