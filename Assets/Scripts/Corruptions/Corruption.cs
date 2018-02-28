using UnityEngine;

namespace ProjectFTP.Corruptions
{
    public abstract class Corruption : ScriptableObject, ICorruption
    {
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private Sprite iconSmall;

        public Sprite Icon
        {
            get { return icon; }
        }

        public Sprite SmallIcon
        {
            get { return iconSmall; }
        }

        public abstract void SetUp();
        public abstract void TearDown();
    }
}
