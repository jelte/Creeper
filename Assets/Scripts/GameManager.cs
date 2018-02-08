using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

	public static int deaths = 0;
	public static float timeTaken = 0f;
	public static bool end = false;
	public PostProcessingProfile deathCameraEffect;

	Character character;

	void Start() {
		end = false;
	}

	void Update() {
		if (end) {
			
			return;
		}
	
		if (CharacterDied) {
			end = true;
			deaths += 1;
			Camera.main.GetComponent<PostProcessingBehaviour> ().enabled = false;
			Camera.main.GetComponent<PostProcessingBehaviour> ().profile = deathCameraEffect;
			Camera.main.GetComponent<PostProcessingBehaviour> ().enabled = true;
			SceneManager.LoadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/DeathScene"), LoadSceneMode.Additive);

		} else {
			timeTaken += Time.deltaTime;
		}
	}

	public static bool CharacterDied {
		get { 
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			return player != null ? player.GetComponent<Character>().Died () : false;
		}
	}

}
