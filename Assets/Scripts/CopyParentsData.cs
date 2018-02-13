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
    public bool copyColor;
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
            float parentTransparensyColor = 255;

            if (parentType == ParentsType.GUIImage)
                parentTransparency = parent.GetComponent<Image>().color.a;
            if (parentType == ParentsType.GUItext)
                parentTransparency = parent.GetComponent<Text>().color.a;
            parentTransparensyColor = parentTransparency;

            if (childType == ChildType.GUIImage)
                gameObject.GetComponent<Image>().color = new Color( gameObject.GetComponent<Image>().color.r,
                                                                    gameObject.GetComponent<Image>().color.g,
                                                                    gameObject.GetComponent<Image>().color.b,
                                                                    parentTransparensyColor);
            if (childType == ChildType.GUItext)

                gameObject.GetComponent<Text>().color =new Color(   gameObject.GetComponent<Text>().color.r, 
                                                                    gameObject.GetComponent<Text>().color.g, 
                                                                    gameObject.GetComponent<Text>().color.b,
                                                                    parentTransparensyColor);
        }

        if (copyColor && parent != null)
        {
            Color parentColor = Color.white;
            if (parentType == ParentsType.GUIImage)
                parentColor = parent.GetComponent<Image>().color;
            if (parentType == ParentsType.GUItext)
                parentColor = parent.GetComponent<Text>().color;

            if (childType == ChildType.GUIImage)
                gameObject.GetComponent<Image>().color = parentColor;
            if (childType == ChildType.GUItext)
                gameObject.GetComponent<Text>().color = parentColor;

        }
	}
}
