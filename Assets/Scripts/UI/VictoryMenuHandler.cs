using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using ProjectFTP.SceneManagement;
using ProjectFTP.Level;
using ProjectFTP.Player;

namespace ProjectFTP.UI
{
    /**
     * Initialize victory text and allow for restart
     */
    public class VictoryMenuHandler : MonoBehaviour
    {
        public Text label;
        public PostProcessingProfile postProcessingProfile;

        void Start()
        {
            Attempt attempt = StackedSceneManager.Active.Get<Attempt>(SceneParameter.ATTEMPT);
            Time.timeScale = 0;
            if (label)
            {
                attempt.Victory();
                LevelStats stats = GameManager.Instance.Profile.GetLevelStats(
                    StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD),
                    StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL)
                );
                label.text = "Time take: " + stats.Minutes + " m " + stats.Seconds + " s.\n";
                label.text += "Attempts: " + stats.Attempts + "\n";
            } else
            {
                attempt.Dead();
            }
            GameManager.SaveAttempt(attempt);

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