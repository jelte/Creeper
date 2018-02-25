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
        public GameObject uiPrefab;

        float timer = 0f;
        float maxTime = 2f;
        GameObject uiGameObject;

        public void Start()
        {
            GameObject.FindObjectOfType<CorruptionManager>().ActionHandler += OnCorruption;
        }

        void OnCorruption(ActiveCorruption corruption, CorruptionState state)
        {
            
            switch (state)
            {
                case CorruptionState.START:
                    
                    uiGameObject = GameObject.Instantiate(uiPrefab, gameObject.transform);
                    uiGameObject.GetComponent<Image>().sprite = corruption.Icon;
                    corruptions.Add(corruption, uiGameObject);
                    initialCanvas(corruption);
                    StartCoroutine(ChangetoSmallIcon(corruption, 3f));
                    break;
                case CorruptionState.END:
                    if (corruptions.TryGetValue(corruption, out uiGameObject))
                    {
                        corruptions.Remove(corruption);
                        Destroy(uiGameObject);
                    }
                    timer = 0;
                    break;
            }
        }

        IEnumerator ChangetoSmallIcon(ActiveCorruption corruption,float time)
        {
            Canvas c = uiGameObject.GetComponent<Transform>().parent.GetComponent<Canvas>();
            yield return new WaitForSeconds(time);
            c.GetComponent<GridLayoutGroup>().cellSize = new Vector2(180,180);
            c.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            uiGameObject.GetComponent<Image>().sprite = corruption.SmallIcon;
        }

        void initialCanvas(ActiveCorruption corruption)
        {
            Canvas c = uiGameObject.GetComponent<Transform>().parent.GetComponent<Canvas>();
            c.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
            c.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1920, 180);

        }
        
        private void Update()
        {
        }
    }
}
