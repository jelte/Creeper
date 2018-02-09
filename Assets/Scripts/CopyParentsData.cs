using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyParentsData : MonoBehaviour {

    public GameObject parent;
    public enum ParentsType
    {
        GUItext,        
        GUIImage,
    }
    public ParentsType parentType;
    public enum ChildType
    {
        GUItext,
        GUIImage,
    }
    public ChildType childType;

    public bool copyTransparensy;
    //public bool copyPosition;

    // Use this for initialization
    void Start () {
        if (parent == null)
        {
            parent = gameObject.transform.parent.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {    
        if (copyTransparensy && parent != null)
        {
            float parentTransparency = 0f;
            if (parentType == ParentsType.GUIImage)
                parentTransparency = parent.GetComponent<Image>().color.a;
            if (parentType == ParentsType.GUItext)
                parentTransparency = parent.GetComponent<Text>().color.a;

            Color parentTransparensyColor = new Color(gameObject.GetComponent<Image>().color.r,
                    gameObject.GetComponent<Image>().color.g,
                    gameObject.GetComponent<Image>().color.b,
                    parentTransparency);

            if (childType == ChildType.GUIImage)
                gameObject.GetComponent<Image>().color = parentTransparensyColor;
            if (childType == ChildType.GUItext)
                gameObject.GetComponent<Text>().color = parentTransparensyColor;
        }
        
	}
}
