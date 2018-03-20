﻿ using ProjectFTP.Level;
using ProjectFTP.Player;
using ProjectFTP.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.UI
{
    public class WorldMenuHandler : MonoBehaviour
    {
        public List<WorldConfig> worlds;
        public GameObject worldPanelPrefab;
        public GameObject backgroundPrefab;
        public GameObject levelPrefab;
        public GameObject connectionPrefab;
        public float slideSpeed = 20f;

        public int worldIndex = 0;
        private List<GameObject> worldPanels = new List<GameObject>();
        private List<GameObject> backgrounds = new List<GameObject>();
        private Vector2 expectedPosition;

        void Start()
        {
            Vector2 canvasSize = GetComponent<CanvasScaler>().referenceResolution;
            float scaleFactor = GetComponent<CanvasScaler>().scaleFactor;

            // Initialize each world panel.
            foreach (WorldConfig world in worlds)
            {
                Vector2 position = new Vector2(canvasSize.x * (worldPanels.Count), 0f);
                GameObject worldPanel = Instantiate(worldPanelPrefab, this.transform);
                worldPanel.name = world.name;
                worldPanel.GetComponent<RectTransform>().anchoredPosition = position;
                worldPanel.GetComponentInChildren<WorldLayoutGenerator>().InitWorld(world, levelPrefab);
                worldPanels.Add(worldPanel);

                GameObject background = Instantiate(backgroundPrefab, this.transform);
                background.transform.SetSiblingIndex(0);
                background.GetComponent<RectTransform>().anchoredPosition = position;
                background.GetComponent<Image>().sprite = world.background;
                backgrounds.Add(background);
            }

            // Initialize the connections between each world.
            foreach (GameObject worldPanel in worldPanels)
            {
                worldPanel.GetComponentInChildren<WorldLayoutGenerator>().InitConnections(connectionPrefab);
            }

            // Set buttons as last so they render on top of everything else.
            transform.Find("Next").transform.SetAsLastSibling();
            transform.Find("Previous").transform.SetAsLastSibling();
            
            // Initialize the first world to be show.
            WorldConfig currentWorld = StackedSceneManager.Active != null ? StackedSceneManager.Active.Get<WorldConfig>(SceneParameter.WORLD) : null;
            if (currentWorld != null)
            {
                worldIndex = worlds.FindIndex(delegate (WorldConfig w)
                {
                    return w.Equals(currentWorld);
                });
            }

            // Enable levels based on the progression in the profile.
            Profile profile = GameManager.Instance.Profile;
            bool first = true;
            foreach (WorldConfig world in worlds)
            {
                foreach (LevelConfig level in world.levels)
                {
                    if (first)
                    {
                        first = false;
                        GameObject.Find(level.name).GetComponent<Button>().interactable = true;
                    }
                    if (profile.Completed(world, level))
                    {
                        GameObject.Find(level.name).GetComponent<Image>().color = new Color(64f, 27f,27f);
                        foreach (LevelConfig connection in level.connections)
                        {
                            GameObject.Find(connection.name).GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }
        }
        
        void Update()
        {
            // Slide to the selected world if it is not in view.
            this.expectedPosition = ExpectedPosition(worldIndex);
            RectTransform world0RectTransform = worldPanels[0].GetComponent<RectTransform>();
            if (world0RectTransform.anchoredPosition.x != expectedPosition.x)
            {
                float difference = Mathf.Abs(world0RectTransform.anchoredPosition.x - expectedPosition.x);
                Vector2 direction = (world0RectTransform.anchoredPosition.x > expectedPosition.x ? Vector2.left : Vector2.right);
                Vector2 displacement = direction * (difference < slideSpeed ? difference : slideSpeed);
                foreach (GameObject panel in worldPanels)
                {
                    panel.GetComponent<RectTransform>().anchoredPosition += displacement;
                }
                foreach (GameObject panel in backgrounds)
                {
                    panel.GetComponent<RectTransform>().anchoredPosition += displacement;
                }
            }
            // Check if the pause button was hit
            if (Input.GetButtonDown("Pause"))
            {
                StackedSceneManager.LoadScene(SceneName.PauseScene);
            }
        }

        public void SlideTo(int worldIndex)
        {
            if (worldIndex >= 0 && worldIndex < worlds.Count)
            {
                this.worldIndex = worldIndex;
            }
        }

        public void NextWorld()
        {
            SlideTo(worldIndex + 1);
        }

        public void PrevWorld()
        {
            SlideTo(worldIndex - 1);
        }

        private Vector2 ExpectedPosition(int index)
        {
            return GetComponent<CanvasScaler>().referenceResolution * -index;
        }
    }
}
