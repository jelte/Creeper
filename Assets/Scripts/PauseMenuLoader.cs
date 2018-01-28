using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuLoader : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Quit")) 
		{
			Application.Quit ();
			Debug.Log ("I HAVE QUIT");
		}
	}
}
