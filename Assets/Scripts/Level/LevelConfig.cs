using ProjectFTP.Corruptions;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Level
{
    [CreateAssetMenu(menuName ="Level/LevelConfig", fileName ="Level 0 - 0")]
    public class LevelConfig : ScriptableObject
    {
        public Sprite icon;
        public List<LevelConfig> connections = new List<LevelConfig>();
        public Texture2D layout;
        public Sprite background;
        public ImageConversionScheme imageConversionScheme;
        public List<Corruption> corruptions = new List<Corruption>();
        public int numberOfActiveCorruptions;
        public List<SpeechBubble> speechTriggers = new List<SpeechBubble>();
    }
}
