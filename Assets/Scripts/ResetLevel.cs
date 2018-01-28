using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
