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

    //for store background music & sounds effect volume
    public float backgroundVolume;
    public float soundEfeectsVolume;
    public AudioSource bgmas;//back ground music audio source
    public AudioSource seas; //sound effect audio source


    void Start() {
		end = false;

        //for music
        bgmas = GetComponent<AudioSource>();
        seas = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        backgroundVolume = bgmas.volume;
        soundEfeectsVolume = seas.volume;
    }

    public void SetBackGroundVolume(float volume)
    {
        backgroundVolume = volume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEfeectsVolume = volume;
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

        //for music 
        bgmas.volume = backgroundVolume;
        seas.volume = soundEfeectsVolume;
    }

	public static bool CharacterDied {
		get { 
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			return player != null ? player.GetComponent<Character>().Died () : false;
		}
	}

}
