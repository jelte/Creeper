using UnityEngine;

namespace ProjectFTP.Corruptions
{
    public class ActiveCorruption
    {
        private readonly Corruption corruption;

        public ActiveCorruption(Corruption corruption)
        {
            this.corruption = corruption;
        }

        public Sprite Icon
        {
            get { return corruption.Icon; }
        }
    }
}
