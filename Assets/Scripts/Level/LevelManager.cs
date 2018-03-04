using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using ProjectFTP.UI;
using ProjectFTP.Corruptions;
using ProjectFTP.SceneManagement;

namespace ProjectFTP.Level
{
    public class LevelManager : MonoBehaviour
    {
        public List<LevelConfig> zoneConfigs;
        public GameObject characterPrefab;
        public PostProcessingProfile cameraProfile;
        
        private LevelLoader loader = new LevelLoader();
        private Progression.Level level;
        private Progression.Attempt attempt;

        void Start()
        {
            LevelConfig levelConfig = StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL);

            Camera.main.GetComponent<PostProcessingBehaviour>().profile = cameraProfile;

            loader.LoadLevel(levelConfig.layout, levelConfig.imageConversionScheme, gameObject.transform);

            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
            GameObject character = Instantiate(characterPrefab, spawnPoint.transform.position, Quaternion.identity);
            character.GetComponent<Character>().ActionHandler += OnDie;
            SceneManager.MoveGameObjectToScene(character, StackedSceneManager.Active.Scene);

            GameObject background = CreateNewObject("Background");
            background.AddComponent<SpriteRenderer>().sprite = levelConfig.background;
            background.AddComponent<FollowPlayer>().StartFollow(character, Vector3.forward * 2);

            Camera.main.gameObject.AddComponent<FollowPlayer>().StartFollow(character, Vector3.back * 10);

            GetComponentInChildren<VictoryTrigger>().ActionHandler += OnFinish;

            gameObject.AddComponent<CorruptionManager>().SetUp(levelConfig);

            attempt = level.Attempt;

            // Ensure time is on
            Time.timeScale = 1.0f;
        }
        
        private GameObject CreateNewObject(string name)
        {
            GameObject gameObject = new GameObject(name);
            SceneManager.MoveGameObjectToScene(gameObject, StackedSceneManager.Active.Scene);
            return gameObject;
        }

        void Update()
        {
            attempt.Tick(Time.deltaTime);
            if (Input.GetButtonDown("Pause"))
            {
                StackedSceneManager.LoadScene(SceneName.PauseScene);
            }

        }

        void OnDie(Character.Action action)
        {
            if (action == Character.Action.DIE)
            {
                GetComponent<CorruptionManager>().TearDown();
                StackedSceneManager.LoadScene(SceneName.DeathScene);
            }
        }

        void OnFinish()
        {
            StackedSceneManager.LoadScene(SceneName.VictoryScene);
        }
    }

    public enum Action { FINISH }
}