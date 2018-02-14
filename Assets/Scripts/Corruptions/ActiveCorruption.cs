using System;
using UnityEngine;

namespace ProjectFTP.Corruptions
{
    public class ActiveCorruption
    {
        private readonly Corruption corruption;
        private bool active = true;

        public ActiveCorruption(Corruption corruption)
        {
            this.corruption = corruption;
            corruption.SetUp();
        }

        public Sprite Icon
        {
            get { return corruption.Icon; }
        }
        
        public bool Active {
            get { return active; }
        }

        internal void TearDown()
        {
            active = false;
            corruption.TearDown();
        }
    }
}
