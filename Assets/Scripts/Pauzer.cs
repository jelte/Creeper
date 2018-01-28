using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauzer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause") && !GameManager.CharacterDied) {
			if (Time.timeScale > 0f) {
				Time.timeScale = 0f;
				SceneManager.LoadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene"), LoadSceneMode.Additive);
			} else {
				SceneManager.GetSceneByBuildIndex (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene")).GetRootGameObjects () [0].GetComponentInChildren<Animator> ().SetTrigger ("hide");
				StartCoroutine (Continue ());
			}
		}        
	}

	IEnumerator Continue()
	{
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 1f;
		SceneManager.UnloadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene"));
	}
}
