using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ProjectFTP.Corruptions;

namespace ProjectFTP.UI
{
    class CorruptionUIHandler : MonoBehaviour
    {
        private Dictionary<ActiveCorruption, GameObject> corruptions = new Dictionary<ActiveCorruption, GameObject>();
        public GameObject uiPrefab;

        public void Start()
        {
            GameObject.FindObjectOfType<CorruptionManager>().ActionHandler += OnCorruption;
        }

        void OnCorruption(ActiveCorruption corruption, CorruptionState state)
        {
            GameObject uiGameObject;
            switch (state)
            {
                case CorruptionState.START:
                    uiGameObject = GameObject.Instantiate(uiPrefab, gameObject.transform);
                    uiGameObject.GetComponent<Image>().sprite = corruption.Icon;
                    corruptions.Add(corruption, uiGameObject);
                    break;
                case CorruptionState.END:
                    if (corruptions.TryGetValue(corruption, out uiGameObject))
                    {
                        corruptions.Remove(corruption);
                        Destroy(uiGameObject);
                    }
                    break;
            }
        }
    }
}
