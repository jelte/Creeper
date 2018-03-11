using UnityEngine;

namespace ProjectFTP.Corruptions
{
    public abstract class Corruption : ScriptableObject, ICorruption
    {
        [SerializeField]
        private Sprite icon;

        public Sprite Icon
        {
            get { return icon; }
        }

        public abstract void SetUp();
        public abstract void TearDown();
    }
}
