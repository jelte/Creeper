using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class PauseMenuLoader : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return) || GamePad.GetState(0).Buttons.Back == ButtonState.Pressed) 
		{
			Application.Quit ();
			Debug.Log ("I HAVE QUIT");
		}
	}
}
