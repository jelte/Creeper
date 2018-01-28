using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauzer : MonoBehaviour {

	int pauseSceneBuildIndex;
    
	void Start() {
		pauseSceneBuildIndex = SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene");
        
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) {
			if (Time.timeScale > 0f) {
				Time.timeScale = 0f;
				SceneManager.LoadSceneAsync (pauseSceneBuildIndex, LoadSceneMode.Additive);
			} else {
				SceneManager.GetSceneByBuildIndex (pauseSceneBuildIndex).GetRootGameObjects () [0].GetComponentInChildren<Animator> ().SetTrigger ("hide");
				StartCoroutine (Continue ());
			}
		}        
	}

	IEnumerator Continue()
	{
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 1f;
		SceneManager.UnloadSceneAsync (pauseSceneBuildIndex);
	}
}
