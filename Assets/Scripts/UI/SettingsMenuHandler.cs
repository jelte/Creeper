using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class SettingsMenuHandler : MonoBehaviour
    {
        Settings settings;

        public void Awake()
        {
            settings = GameManager.Instance.Profile.Settings;
            foreach (Slider slider in GetComponentsInChildren<Slider>())
            {
                switch (slider.gameObject.transform.parent.gameObject.name)
                {
                    case "BackgroundMusic":
                        slider.value = settings.BackgroundMusicVolume;
                        break;
                    case "SoundEffects":
                        slider.value = settings.SoundEffectsVolume;
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
                        settings.BackgroundMusicVolume = slider.value;
                        break;
                    case "SoundEffects":
                        settings.SoundEffectsVolume = slider.value;
                        break;
                }
            }
            Close();
        }

        private void Close()
        {
            StackedSceneManager.UnloadScene(SceneName.SettingsScene);
        }
    }
}