using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using ProjectFTP.SceneManagement;

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
            GameManager.Instance.Profile.CurrentStoryModeLevel.Attempt.Victory();
            if (label)
            {
                float timeTaken = GameManager.Instance.Profile.CurrentStoryModeLevel.TimeTaken;
                int attempts = GameManager.Instance.Profile.CurrentStoryModeLevel.Attempts;
                label.text = "Time take: " + Mathf.Floor(timeTaken / 60) + " m " + Mathf.Round(timeTaken % 60) + " s.\n";
                label.text += "Attempts: " + attempts + "\n";
            }

            Camera.main.GetComponent<PostProcessingBehaviour>().profile = postProcessingProfile;
        }

        // Update is called once per frame
        void Update()
        {
            Time.timeScale = 0;
            if (Input.anyKey && timeDelay < 0.0f)
            {
                Time.timeScale = 1;
                StackedSceneManager.LoadScene(SceneName.MainMenu);
            }
            timeDelay -= Time.unscaledDeltaTime;
        }
    }
}