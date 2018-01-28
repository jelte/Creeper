using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauzer : MonoBehaviour {

	bool hiding = false;
	float timer = 0.5f;
	AsyncOperation loadOperation;


	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause") && !GameManager.CharacterDied) {
			if (Time.timeScale > 0f) {
				Time.timeScale = 0f;
				loadOperation = SceneManager.LoadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene"), LoadSceneMode.Additive);
			} else if (!hiding && loadOperation != null && loadOperation.isDone) {
				hiding = true;
				if (SceneManager.GetSceneByBuildIndex (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene")).GetRootGameObjects ().Length > 0) {
					SceneManager.GetSceneByBuildIndex (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene")).GetRootGameObjects () [0].GetComponentInChildren<Animator> ().SetTrigger ("hide");
				}
				StartCoroutine (Continue ());
			}
		}
	}

	IEnumerator Continue()
	{
		yield return new WaitForSecondsRealtime(0.2f);
		Time.timeScale = 1f;
		SceneManager.UnloadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/PauseScene"));
		hiding = false;
	}
}
