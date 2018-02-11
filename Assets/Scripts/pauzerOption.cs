using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauzerOption : MonoBehaviour {


    Animator ani;
    public bool pauseOptionPanelShow = false;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ani.SetBool("OPS", pauseOptionPanelShow);		
	}
}
