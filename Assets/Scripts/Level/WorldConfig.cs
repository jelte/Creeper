using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Level
{
    [CreateAssetMenu(menuName = "Level/WorldConfig", fileName = "World 0")]
    public class WorldConfig : ScriptableObject
    {
        public int world;
        public List<LevelConfig> levels;
        public Sprite background;
    }
}