using ProjectFTP.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace ProjectFTP.UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        public PostProcessingProfile postProcessingProfile;

        void Awake()
        {
            Camera.main.GetComponent<PostProcessingBehaviour>().profile = postProcessingProfile;
        }

        public void StoryMode()
        {
            StackedSceneManager.LoadScene(SceneName.WorldScene, new Dictionary<SceneParameter, object>
            {
                { SceneParameter.WORLD, 0 }
            });
        }

        public void Settings()
        {
            StackedSceneManager.LoadScene(SceneName.SettingsScene);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
