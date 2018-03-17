using UnityEngine;

namespace ProjectFTP.Level
{
    /**
     * Load victory scene when player enters trigger
     */
    public class VictoryTrigger : MonoBehaviour
    {
        #region event handlers
        public delegate void OnVictoryEvent();

        public event OnVictoryEvent ActionHandler;
        #endregion
        
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player" && ActionHandler != null)
            {
                ActionHandler();
            }
        }
    }
}