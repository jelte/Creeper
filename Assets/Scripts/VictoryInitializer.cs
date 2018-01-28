using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Initialize victory text and allow for restart
 */
public class VictoryInitializer : MonoBehaviour {

	public Text label;

	// Use this for initialization
	void Start () {
		label.text = "Time take: " + GameManager.timeTaken + "\n";
		label.text += "Deaths: " + GameManager.deaths + "\n";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			GameManager.deaths = 0;
			GameManager.timeTaken = 0f;
			Time.timeScale = 1;
		}
	}
}
