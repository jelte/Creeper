using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

/**
 * Initialize victory text and allow for restart
 */
public class VictoryInitializer : MonoBehaviour {

	public Text label;
	public PostProcessingProfile postProcessingProfile;

	float timeDelay = 1.0f;

	// Use this for initialization
	void Start () {
		label.text = "Time take: " + Mathf.Floor(GameManager.timeTaken/60) + " m " + Mathf.Round(GameManager.timeTaken%60) + " s.\n";
		label.text += "Deaths: " + GameManager.deaths;
		Camera.main.GetComponent<PostProcessingBehaviour> ().profile = postProcessingProfile;
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = 0;
		if (Input.anyKey && timeDelay < 0.0f) {
			GameManager.deaths = 0;
			GameManager.timeTaken = 0f;
			Time.timeScale = 1;
			SceneManager.LoadScene (SceneUtility.GetBuildIndexByScenePath("Scenes/Level1Scene"));
		}
		timeDelay -= Time.unscaledDeltaTime;
	}
}
