using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP.Level;
using System;

namespace ProjectFTP.Corruptions
{
    public class CorruptionManager : MonoBehaviour
    {
        public LevelConfig levelConfig;

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
                    StartCoroutine(ApplyCorruption(levelConfig.corruptions[new System.Random().Next(levelConfig.corruptions.Count)], 10));
                }
            }
        }

        IEnumerator ApplyCorruption(Corruption corruption, int seconds)
        {
            ActiveCorruption wrapped = new ActiveCorruption(corruption);
            activeCorruptions.Add(wrapped);
            TriggerEvent(wrapped, CorruptionState.START);
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
    }
}
