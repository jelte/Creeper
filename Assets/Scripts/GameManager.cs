using UnityEngine;
using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using ProjectFTP.Sound;

namespace ProjectFTP
{
    public class GameManager : MonoBehaviour
    {
        private const string SETTINGS_BACKGROUND_MUSIC_VOLUME = "bg_volume";
        private const string SETTINGS_SOUND_EFFECTS_VOLUME = "sfx_volume";
        static private GameManager instance;
        ProfileLoader profileLoader;
        private int world = 0;
        private int level = 0;

        public void Start()
        {
            instance = this;
            profileLoader = new ProfileLoader(Application.persistentDataPath + "/profiles.dat");

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
    }
}
