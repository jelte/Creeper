using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectFTP.Level;

namespace ProjectFTP.Corruptions
{
    public class CorruptionManager : MonoBehaviour
    {
        public LevelConfig levelConfig;
        private int activeCorruptions = 0;
        
        #region event handlers
        public delegate void OnCorruption(ActiveCorruption corruption, CorruptionState state);

        public event OnCorruption ActionHandler;
        #endregion

        void Update()
        {
            if (levelConfig)
            {
                for (int i = activeCorruptions; i < levelConfig.numberOfActiveCorruptions; i++)
                {
                    StartCoroutine(ApplyCorruption(levelConfig.corruptions[new System.Random().Next(levelConfig.corruptions.Count)], 10));
                }
            }
        }

        IEnumerator ApplyCorruption(Corruption corruption, int seconds)
        {
            activeCorruptions++;
            corruption.SetUp();
            ActiveCorruption wrapped = new ActiveCorruption(corruption);
            TriggerEvent(wrapped, CorruptionState.START);
            yield return new WaitForSeconds(seconds);
            TriggerEvent(wrapped, CorruptionState.END);
            corruption.TearDown();
            activeCorruptions--;
        }

        private void TriggerEvent(ActiveCorruption corruption, CorruptionState state)
        {
            if (ActionHandler != null)
            {
                ActionHandler(corruption, state);
            }
        }
    }
}
