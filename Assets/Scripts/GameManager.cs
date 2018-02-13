using UnityEngine;
using ProjectFTP.Player;
using ProjectFTP.SceneManagement;

namespace ProjectFTP
{
    public class GameManager : MonoBehaviour
    {
        static private GameManager instance;
        ProfileLoader profileLoader;

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
