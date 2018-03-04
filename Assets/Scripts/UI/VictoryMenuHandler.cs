using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using ProjectFTP.SceneManagement;
using ProjectFTP.Level;

namespace ProjectFTP.UI
{
    /**
     * Initialize victory text and allow for restart
     */
    public class VictoryMenuHandler : MonoBehaviour
    {
        public Text label;
        public PostProcessingProfile postProcessingProfile;

        private float timeDelay = 1.0f;

        // Use this for initialization
        void Start()
        {
            Time.timeScale = 0;
            if (label)
            {
                GameManager.Instance.Profile.CurrentStoryModeLevel.Attempt.Victory();
                float timeTaken = GameManager.Instance.Profile.CurrentStoryModeLevel.TimeTaken;
                int attempts = GameManager.Instance.Profile.CurrentStoryModeLevel.Attempts;
                label.text = "Time take: " + Mathf.Floor(timeTaken / 60) + " m " + Mathf.Round(timeTaken % 60) + " s.\n";
                label.text += "Attempts: " + attempts + "\n";
            } else
            {

                GameManager.Instance.Profile.CurrentStoryModeLevel.Attempt.Dead();
            }

            Camera.main.GetComponent<PostProcessingBehaviour>().profile = postProcessingProfile;
        }

        public void Next()
        {
            StackedSceneManager.LoadScene(SceneName.WorldScene, new Dictionary<SceneParameter, object>() {
                {SceneParameter.WORLD, StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD) }
            });
        }

        public void Retry()
        {
            StackedSceneManager.ReloadScene();
        }

        public void MainMenu()
        {
            StackedSceneManager.LoadScene(SceneName.MainMenu);
        }
    }
}