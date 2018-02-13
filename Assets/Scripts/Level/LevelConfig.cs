using ProjectFTP.Corruptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProjectFTP.Level
{
    [CreateAssetMenu(menuName ="Level/LevelConfig", fileName ="Level 0 - 0")]
    public class LevelConfig : ScriptableObject
    {
        public int world;
        public int zone;
        public Texture2D layout;
        public Sprite background;
        public ImageConversionScheme imageConversionScheme;
        public List<Corruption> corruptions = new List<Corruption>();
        public int numberOfActiveCorruptions;
    }
}
