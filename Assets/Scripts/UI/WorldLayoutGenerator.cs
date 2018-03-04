using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectFTP.Level
{
    class WorldLayoutGenerator : MonoBehaviour
    {
        private WorldConfig world;
        private GameObject levelPrefab;
        private GameObject connectionPrefab;
        
        public void InitWorld(WorldConfig world, GameObject levelPrefab)
        {
            this.world = world;
            this.levelPrefab = levelPrefab;
            transform.parent.Find("TitlePanel").GetComponentInChildren<Text>().text = world.name;
            InitCluster(this.gameObject, world.levels);
        }
        
        /**
         * Build connections after all worlds have been generated in order to set up cross world connections
         */
        public void InitConnections(GameObject connectionPrefab)
        {
            this.connectionPrefab = connectionPrefab;
            foreach (LevelConfig levelConfig in world.levels)
            {
                foreach (LevelConfig connected in levelConfig.connections)
                {
                    CreateConnection(levelConfig, connected);
                }
            }
        }
        
        private void InitCluster(GameObject cluster, List<LevelConfig> levels)
        {
            RectTransform clusterTransform = cluster.GetComponent<RectTransform>();
            Vector2 size = clusterTransform.rect.size;
            // If only one level place it in the middle
            if (levels.Count == 1)
            {
                CreateLevelButton(clusterTransform, Vector2.zero, levels[0]);
            }
            // Scatter the levels around the canvas
            else
            {
                int count = levels.Count;
                if (count == 7)
                {
                    count = 6;
                }
                float arc = 360 / count;

                for (float angle = 0; (int)(angle / arc) < count; angle += arc)
                {
                    CreateLevelButton(clusterTransform, GetDirectionFromAngle(180 + angle, size / 3), levels[(int)(angle / arc)]);
                }
                if (levels.Count == 7)
                {
                    CreateLevelButton(clusterTransform, Vector2.zero, levels[count]);
                }
            }
        }

        /**
         * Create a level button
         */
        private void CreateLevelButton(RectTransform clusterTransform, Vector2 position, LevelConfig levelConfig)
        {
            GameObject level = Instantiate(levelPrefab);
            level.name = levelConfig.name;
            RectTransform levelTransform = level.GetComponent<RectTransform>();
            levelTransform.SetParent(clusterTransform);
            levelTransform.anchoredPosition = position;
            level.transform.Find("Icon").GetComponent<Image>().sprite = levelConfig.icon;
        }

        private void CreateConnection(LevelConfig a, LevelConfig b)
        {
            GameObject connection = Instantiate(connectionPrefab, this.transform);
            connection.transform.SetSiblingIndex(0);
            RectTransform connectionTransform = connection.GetComponent<RectTransform>();
            GameObject aGameObject = GameObject.Find(a.name);
            GameObject bGameObject = GameObject.Find(b.name);
            
            RectTransform aRectTransform = aGameObject.GetComponent<RectTransform>();
            RectTransform bRectTransform = bGameObject.GetComponent<RectTransform>();
            Vector2 vector = bRectTransform.position - aRectTransform.position;
            Vector2 vectorPos = bRectTransform.anchoredPosition - aRectTransform.anchoredPosition;
            if (aGameObject.transform.parent != bGameObject.transform.parent)
            {
                Vector2 aPosition = aRectTransform.anchoredPosition 
                    + aRectTransform.parent.GetComponent<RectTransform>().anchoredPosition 
                    + aRectTransform.parent.parent.GetComponent<RectTransform>().anchoredPosition;
                Vector2 bPosition = bRectTransform.anchoredPosition 
                    + bRectTransform.parent.GetComponent<RectTransform>().anchoredPosition 
                    + bRectTransform.parent.parent.GetComponent<RectTransform>().anchoredPosition;
                vectorPos = bPosition - aPosition;
            }
            float angle = Mathf.Atan2(vector.y, vector.x);
            
            connectionTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vectorPos.magnitude);
            connectionTransform.SetPositionAndRotation(aRectTransform.position, Quaternion.EulerAngles(0f, 0f, angle));
            connectionTransform.localScale = new Vector3(1f, connectionTransform.localScale.y, connectionTransform.localScale.z);
        }

        private Vector2 GetDirectionFromAngle(float angle, Vector2 size)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * size.x, Mathf.Sin(angle * Mathf.Deg2Rad) * size.y);
        }
    }
}