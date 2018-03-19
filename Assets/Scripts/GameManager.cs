using UnityEngine;
using ProjectFTP.Player;
using ProjectFTP.SceneManagement;

namespace ProjectFTP
{
    public class GameManager : MonoBehaviour
    {
        private const string SETTINGS_BACKGROUND_MUSIC_VOLUME = "bg_volume";
        private const string SETTINGS_SOUND_EFFECTS_VOLUME = "sfx_volume";
        static private GameManager instance;
        private ProfileLoader profileLoader;

        void Start()
        {
            instance = this;
            // Instantiate the profile loader
            profileLoader = new ProfileLoader(Application.persistentDataPath + "/profiles.dat");
            // Load the main scene
            StackedSceneManager.LoadScene(SceneName.MainMenu);
        }

        public Profile Profile
        {
            get { return profileLoader.ActiveProfile; }
        }

        public static float BackgroundMusicVolume
        {
            get { return PlayerPrefs.GetFloat(SETTINGS_BACKGROUND_MUSIC_VOLUME); }
            set {
                PlayerPrefs.SetFloat(SETTINGS_BACKGROUND_MUSIC_VOLUME, value);
                BackgroundMusic.Instance.UpdateVolume();
            }
        }

        public static float SoundEffectsVolume
        {
            get { return PlayerPrefs.GetFloat(SETTINGS_SOUND_EFFECTS_VOLUME); }
            set { PlayerPrefs.SetFloat(SETTINGS_SOUND_EFFECTS_VOLUME, value); }
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new System.Exception("GameManager not instanciated.");
                }
                return instance;
            }
        }

        public static void SaveAttempt(Attempt attempt)
        {
            // Add the attempt to the profile
            Instance.Profile.AddAttempt(attempt);
            // Save the profile
            Instance.profileLoader.Update();
        }
    }
}
