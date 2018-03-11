using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP.Level;

namespace ProjectFTP.Corruptions
{
    public class CorruptionManager : MonoBehaviour
    {
        public LevelConfig levelConfig;
        public int minDelay = 5;
        public int maxDelay = 20;
        public int minDuration = 10;
        public int maxDuration = 20;

        private List<ActiveCorruption> activeCorruptions = new List<ActiveCorruption>();
        
        #region event handlers
        public delegate void OnCorruption(ActiveCorruption corruption, CorruptionState state);

        public event OnCorruption ActionHandler;
        #endregion
        
        void Update()
        {
            if (levelConfig && Time.timeScale > 0)
            {
                for (int i = activeCorruptions.Count; i < levelConfig.numberOfActiveCorruptions; i++)
                {
                    StartCoroutine(ApplyCorruption(
                        levelConfig.corruptions[Random.Range(0, levelConfig.corruptions.Count)],
                        Random.Range(minDelay, maxDelay), 
                        Random.Range(minDuration, maxDuration)
                    ));
                }
            }
        }

        IEnumerator ApplyCorruption(Corruption corruption, int delay, int seconds)
        {
            ActiveCorruption wrapped = new ActiveCorruption(corruption);
            activeCorruptions.Add(wrapped);
            yield return new WaitForSeconds(delay);
            TriggerEvent(wrapped, CorruptionState.START);
            wrapped.SetUp();
            yield return new WaitForSeconds(seconds);
            if (wrapped.Active)
            {
                TriggerEvent(wrapped, CorruptionState.END);
                wrapped.TearDown();
                activeCorruptions.Remove(wrapped);
            }
        }
 
        private void TriggerEvent(ActiveCorruption corruption, CorruptionState state)
        {
            if (ActionHandler != null)
            {
                ActionHandler(corruption, state);
            }
        }

        public void TearDown()
        {
            foreach (ActiveCorruption corruption in activeCorruptions) {
                TriggerEvent(corruption, CorruptionState.END);
                corruption.TearDown();
            }
            activeCorruptions.Clear();
        }

        public void SetUp(LevelConfig levelConfig)
        {
            this.levelConfig = levelConfig;
        }

        void OnDestroy()
        {
            TearDown();    
        }
    }
}
