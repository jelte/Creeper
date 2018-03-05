using ProjectFTP.Level;
using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.UI
{
    public class LevelButtonHandler : MonoBehaviour
    {
        public WorldConfig world;
        public LevelConfig level;
        
        public void LoadLevel()
        {
            StackedSceneManager.LoadScene(SceneName.LevelScene, new Dictionary<SceneParameter, object> {
                {SceneParameter.WORLD, world},
                {SceneParameter.LEVEL, level}
            });
        }
    }
}
