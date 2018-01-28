using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Load victory scene when player enters trigger
 */
public class VictoryTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			SceneManager.LoadScene (SceneUtility.GetBuildIndexByScenePath ("Scenes/VictoryScene"), LoadSceneMode.Additive);
		}
	}
}
