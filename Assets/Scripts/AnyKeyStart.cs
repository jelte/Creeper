﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKeyStart : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
		
        if(Input.anyKey)
        {
			SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath("Scenes/Level1Scene"));
        }
	}
}