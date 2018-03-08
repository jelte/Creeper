using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ProjectFTP.Corruptions;
using System.Collections;

namespace ProjectFTP.UI
{
    class CorruptionUIHandler : MonoBehaviour
    {
        private Dictionary<ActiveCorruption, GameObject> corruptions = new Dictionary<ActiveCorruption, GameObject>();
        public GameObject uiPrefab = null;

        public void Start()
        {
            GameObject.FindObjectOfType<CorruptionManager>().ActionHandler += OnCorruption;
        }

        void OnCorruption(ActiveCorruption corruption, CorruptionState state)
        {
            switch (state)
            {
                case CorruptionState.START:
                    corruptions.Add(corruption, GameObject.Instantiate(uiPrefab, gameObject.transform));
                    StartCoroutine(Minimize(corruption, 3f));
                    break;
                case CorruptionState.END:
                    GameObject uiGameObject;
                    if (corruptions.TryGetValue(corruption, out uiGameObject))
                    {
                        corruptions.Remove(corruption);
                        Destroy(uiGameObject);
                    }
                    break;
            }
        }

        IEnumerator Minimize(ActiveCorruption corruption, float time)
        {
            GameObject uiGameObject = corruptions[corruption];
            Image image = uiGameObject.GetComponent<Image>();
            // Show in banner
            image.sprite = corruption.Icon;
            GridLayoutGroup gridLayoutGroup = uiGameObject.GetComponent<GridLayoutGroup>();
            gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            gridLayoutGroup.cellSize = new Vector2(1920, 180);
            // Wait for <time>
            yield return new WaitForSeconds(time);
            // Minize
            gridLayoutGroup.cellSize = new Vector2(180,180);
            gridLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            image.sprite = corruption.SmallIcon;
        }
    }
}
