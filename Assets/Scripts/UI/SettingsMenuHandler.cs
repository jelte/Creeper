using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class SettingsMenuHandler : MonoBehaviour
    {
        public void Awake()
        {
            // Set the current value for each slider
            foreach (Slider slider in GetComponentsInChildren<Slider>())
            {
                switch (slider.gameObject.transform.parent.gameObject.name)
                {
                    case "BackgroundMusic":
                        slider.value = GameManager.BackgroundMusicVolume;
                        break;
                    case "SoundEffects":
                        slider.value = GameManager.SoundEffectsVolume;
                        break;
                }
            }
        }

        public void Cancel()
        {
            Close();
        }
        
        public void Apply()
        {
            // Save each slider value
            foreach (Slider slider in GetComponentsInChildren<Slider>())
            {
                switch (slider.gameObject.transform.parent.gameObject.name)
                {
                    case "BackgroundMusic":
                        GameManager.BackgroundMusicVolume = slider.value;
                        break;
                    case "SoundEffects":
                        GameManager.SoundEffectsVolume = slider.value;
                        break;
                }
            }
            // Close the settings window
            Close();
        }

        private void Close()
        {
            // Unload the settings scene
            StackedSceneManager.UnloadScene(SceneName.SettingsScene);
        }
    }
}