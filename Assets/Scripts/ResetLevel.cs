using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour {

    public bool isAnyKeyToRetry;

	float timeDelay = 1f;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }


	// Update is called once per frame
	void Update () {

		if (isAnyKeyToRetry && Input.anyKeyDown && timeDelay < 0.0f)
        {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} 
		timeDelay -= Time.unscaledDeltaTime;
	}
}
