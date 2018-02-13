using ProjectFTP.SceneManagement;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace ProjectFTP.UI
{
    public class MainMenuHandler : MonoBehaviour
    {
        public PostProcessingProfile postProcessingProfile;

        void Update()
        {
            Camera.main.GetComponent<PostProcessingBehaviour>().profile = postProcessingProfile;
        }

        public void StoryMode()
        {
            StackedSceneManager.LoadScene(SceneName.StoryModeScene);
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
