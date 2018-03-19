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
                // Fill up the active corruption list with new corruptions.
                for (int i = activeCorruptions.Count; i < levelConfig.numberOfActiveCorruptions; i++)
                {
                    // Start a new corruption
                    StartCoroutine(ApplyCorruption(
                        // Randomize the corruption
                        levelConfig.corruptions[Random.Range(0, levelConfig.corruptions.Count)],
                        // Randomize the delay
                        Random.Range(minDelay, maxDelay), 
                        // Randomize the duration
                        Random.Range(minDuration, maxDuration)
                    ));
                }
            }
        }

        IEnumerator ApplyCorruption(Corruption corruption, int delay, int seconds)
        {
            // Wrap the corruption.
            ActiveCorruption wrapped = new ActiveCorruption(corruption);
            // add it to the active corruptions list.
            activeCorruptions.Add(wrapped);
            // Wait for the delay to pass.
            yield return new WaitForSeconds(delay);
            // Notify listeners that hte corruption is starting.
            TriggerEvent(wrapped, CorruptionState.START);
            // Start the corruption.
            wrapped.SetUp();
            // With for the duration to pass.
            yield return new WaitForSeconds(seconds);
            // if the corruption is still active (to cancelled due to victory, death or leaving the game.
            if (wrapped.Active)
            {
                // notify listeners that the corruption has ended.
                TriggerEvent(wrapped, CorruptionState.END);
                // stop the corruption.
                wrapped.TearDown();
                // Remove the corruption from the list
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
                // notify listeners that the corruption has ended.
                TriggerEvent(corruption, CorruptionState.END);
                // stop the corruption.
                corruption.TearDown();
            }
            // Remove all corruptions for the list.
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
