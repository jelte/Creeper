using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using ProjectFTP.UI;
using ProjectFTP.Corruptions;
using ProjectFTP.SceneManagement;
using ProjectFTP.Player;

namespace ProjectFTP.Level
{
    public class LevelManager : MonoBehaviour
    {
        public List<LevelConfig> zoneConfigs;
        public GameObject characterPrefab;
        public PostProcessingProfile cameraProfile;
        public float saveTimer = 10.0f;
        
        private LevelLoader loader = new LevelLoader();
        private Attempt attempt;

        private float lastSave = 10.0f;

        private GameObject CreateNewObject(string name)
        {
            GameObject gameObject = new GameObject(name);
            SceneManager.MoveGameObjectToScene(gameObject, StackedSceneManager.Active.Scene);
            return gameObject;
        }
        
        void Start()
        {
            LevelConfig levelConfig = StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL);
            attempt = new Attempt(StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD), levelConfig);

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

            // Ensure time is on
            Time.timeScale = 1.0f;
            GameManager.SaveAttempt(attempt);
        }
        
        void Update()
        {
            // Update attempt time
            attempt.Tick(Time.deltaTime);
            // Update last save counter
            lastSave -= Time.deltaTime;
            // Save attempt when interval has been exceeded.
            if (lastSave < 0.0f)
            {
                GameManager.SaveAttempt(attempt);
                lastSave = Time.deltaTime;
            }
            // Check if the pause button was hit
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
                StackedSceneManager.LoadScene(SceneName.DeathScene, new Dictionary<SceneParameter, object>() {
                    { SceneParameter.WORLD, StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD)},
                    { SceneParameter.LEVEL, StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL)},
                    { SceneParameter.ATTEMPT, attempt}
                });
            }
        }

        void OnFinish()
        {
            GetComponent<CorruptionManager>().TearDown();
            StackedSceneManager.LoadScene(SceneName.VictoryScene, new Dictionary<SceneParameter, object>() {
                { SceneParameter.WORLD, StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD)},
                { SceneParameter.LEVEL, StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL)},
                { SceneParameter.ATTEMPT, attempt}
            });
        }
    }

    public enum Action { FINISH }
}