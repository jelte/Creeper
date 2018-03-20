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
    public enum Action { FINISH }

    public class LevelManager : MonoBehaviour
    {
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
            // Get the level config from the active scene parameters.
            LevelConfig levelConfig = StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL);

            // Create a new attempt.
            attempt = new Attempt(StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD), levelConfig);
        
            // Load the level layout.
            loader.LoadLevel(levelConfig.layout, levelConfig.imageConversionScheme, gameObject.transform);

            // Find the spawnpoint.
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            // Spawn the character at the spawnpoint.
            GameObject character = Instantiate(characterPrefab, spawnPoint.transform.position, Quaternion.identity);
            // Add the listener for when the character dies.
            character.GetComponent<Character>().ActionHandler += OnDie;
            // make sure the character is attached to the active scene.
            SceneManager.MoveGameObjectToScene(character, StackedSceneManager.Active.Scene);

            // Instantiate the background
            GameObject background = CreateNewObject("Background");
            background.AddComponent<SpriteRenderer>().sprite = levelConfig.background;
            background.AddComponent<FollowPlayer>().StartFollow(character, Vector3.forward * 2);

            // Reset the camera post processing effects and make it follow the character.
            Camera.main.GetComponent<PostProcessingBehaviour>().profile = cameraProfile;
            Camera.main.gameObject.AddComponent<FollowPlayer>().StartFollow(character, Vector3.back * 10);

            // Add a listener for when the level is completed
            GetComponentInChildren<VictoryTrigger>().ActionHandler += OnFinish;

            // Set up the corruptions
            gameObject.AddComponent<CorruptionManager>().SetUp(levelConfig);

            // Set up the speech triggers
            levelConfig.speechTriggers.ForEach(delegate(SpeechBubble speechBubble) {
                GameObject trigger = new GameObject();
                trigger.transform.parent = transform;
                trigger.AddComponent<SpeechBubbleTrigger>().speechBubble = speechBubble;
            });

            // Ensure time is on
            Time.timeScale = 1.0f;

            // Save the attempt
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

        void Transition(SceneName scene)
        {
            // Hide all UI
            foreach (Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if (canvas.gameObject.name != "Canvas")
                {
                    canvas.enabled = false;
                }
            }
            // Stop all corruptions
            GetComponent<CorruptionManager>().TearDown();
            // Load the death scene
            StackedSceneManager.LoadScene(scene, new Dictionary<SceneParameter, object>() {
                { SceneParameter.WORLD, StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD)},
                { SceneParameter.LEVEL, StackedSceneManager.Active.Get<LevelConfig>(SceneParameter.LEVEL)},
                { SceneParameter.ATTEMPT, attempt}
            });
        }

        void OnDie(Character.Action action)
        {
            if (action == Character.Action.DIE)
            {
                Transition(SceneName.DeathScene);
            }
        }

        void OnFinish()
        {
            Transition(SceneName.VictoryScene);
        }

        void OnDestroy()
        {
            // do some clean up when the level manager gets destroyed.
            if (Camera.main != null && Camera.main.gameObject != null && Camera.main.gameObject.GetComponent<FollowPlayer>() != null)
            {
                Destroy(Camera.main.gameObject.GetComponent<FollowPlayer>());
            }
        }
    }
}