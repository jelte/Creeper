using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public Text NameArea;
    public int NameMessage;
    public string[] Name;


    public Text TextArea;
    public int showingMessage;
    public string[] message;
	
	// Update is called once per frame
	void Update () {
        if (Name != null)
            NameArea.text = Name[NameMessage];
        if (message != null)
            TextArea.text = message[showingMessage];
    }
}
