using System;
using UnityEngine;

namespace ProjectFTP.Level
{
    [Serializable]
    public class SpeechBubble
    {
        public Vector3 position;
        public float Radius;
        [TextArea]
        public string text;
    }
}
