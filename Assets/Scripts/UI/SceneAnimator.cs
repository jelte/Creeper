using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProjectFTP.UI
{
    class SceneAnimator : MonoBehaviour
    {
        public enum State { STARTED, FINISHED };
        #region event handlers
        public delegate void OnAnimationEvent(State action);

        public event OnAnimationEvent ActionHandler;
        #endregion

        Animator animator;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("UI_Hide"))
            {
                if (state.normalizedTime > state.length) {
                    TriggerEvent(State.FINISHED);
                }
            }
        }

        public void Hide()
        {
            animator.SetTrigger("Hide");
            TriggerEvent(State.STARTED);
        }

        private void TriggerEvent(State state)
        {
            if (ActionHandler != null)
            {
                ActionHandler(state);
            }
        }
    }
}
