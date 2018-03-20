using System;
using UnityEngine;

namespace ProjectFTP.Level
{
    [Serializable]
    public class SpeechBubble
    {
        // position where the trigger should be spawned.
        public Vector3 position;
        // radius of the trigger.
        public float Radius;
        // text that should be shown.
        [TextArea]
        public string text;
    }
}
