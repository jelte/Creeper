using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SpeechBubble {
    public Vector3 position;
    public float Radius;
    [TextArea]  
    public string text;
}
