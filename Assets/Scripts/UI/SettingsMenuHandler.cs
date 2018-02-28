using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class SettingsMenuHandler : MonoBehaviour
    {
        Settings settings;

        public void Awake()
        {
            Debug.Log(GameManager.BackgroundMusicVolume);
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

            Debug.Log(
                        GameManager.BackgroundMusicVolume);
            Close();
        }

        private void Close()
        {
            StackedSceneManager.UnloadScene(SceneName.SettingsScene);
        }
    }
}