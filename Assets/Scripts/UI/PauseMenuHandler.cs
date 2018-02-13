using ProjectFTP.SceneManagement;
using UnityEngine;

namespace ProjectFTP.UI
{
    public class PauseMenuHandler : MonoBehaviour
    {
        void Start()
        {
            Time.timeScale = 0.0f;
        }
       
        public void Resume()
        {
            Time.timeScale = 1.0f;
            StackedSceneManager.UnloadScene(SceneName.PauseScene);
        }

        public void Options()
        {
            StackedSceneManager.LoadScene(SceneName.SettingsScene);
        }

        public void Quit()
        {
            Time.timeScale = 1.0f;
            StackedSceneManager.LoadScene(SceneName.MainMenu);
        }
    }
}
