using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

	float timeDelay = 1f;

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && timeDelay < 0.0f) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} 
		timeDelay -= Time.unscaledDeltaTime;
	}
}
