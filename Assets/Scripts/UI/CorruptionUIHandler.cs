using System.Collections.Generic;
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
                    {
                        GameObject uiGameObject = GameObject.Instantiate(uiPrefab, gameObject.transform);
                        Rect rect = uiGameObject.GetComponent<RectTransform>().rect;
                        rect.width = GetComponent<RectTransform>().rect.width;
                        uiGameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = corruption.Icon;
                        uiGameObject.GetComponentInChildren<Text>().text = corruption.Name;
                        corruptions.Add(corruption, uiGameObject);
                        StartCoroutine(Minimize(uiGameObject));
                        break;
                    } 
                case CorruptionState.END:
                    {
                        GameObject uiGameObject;
                        if (corruptions.TryGetValue(corruption, out uiGameObject))
                        {
                            corruptions.Remove(corruption);
                            Destroy(uiGameObject);
                        }
                        break;
                    }
            }
        }

        IEnumerator Minimize(GameObject uiGameObject)
        {
            yield return new WaitForSeconds(3f);
            uiGameObject.GetComponent<Animation>().Play();
        }
    }
}
