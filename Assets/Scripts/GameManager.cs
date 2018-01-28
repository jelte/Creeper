using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

	public static int deaths = 0;
	public Character character;
	public bool end = false;
	public PostProcessingProfile deathCameraEffect;

	void Update() {
		if (CharacterDied) {
			if (!end) {
				GameManager.deaths += 1;
				end = true;
				Camera.main.GetComponent<PostProcessingBehaviour> ().enabled = false;
				Camera.main.GetComponent<PostProcessingBehaviour> ().profile = deathCameraEffect;
				Camera.main.GetComponent<PostProcessingBehaviour> ().enabled = true;
				SceneManager.LoadSceneAsync (SceneUtility.GetBuildIndexByScenePath ("Scenes/DeathScene"), LoadSceneMode.Additive);
			}
		}
	}

	public bool CharacterDied {
		get { 
			if (character == null) {
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				if (player != null) {
					character = player.GetComponent<Character> ();
				}
			}
			return character != null ? character.Died () : false;
		}
	}

}
