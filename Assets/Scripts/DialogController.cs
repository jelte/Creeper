using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public Text TextArea;

    public int showingMessage;
    public string[] message;
	
	// Update is called once per frame
	void Update () {
        if (message != null)
            TextArea.text = message[showingMessage];
    }
}
