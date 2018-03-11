using System;
using UnityEngine;

namespace ProjectFTP.Corruptions
{
    public class ActiveCorruption
    {
        private readonly Corruption corruption;
        private bool active;

        public ActiveCorruption(Corruption corruption)
        {
            this.corruption = corruption;
        }

        public Sprite Icon
        {
            get { return corruption.Icon; }
        }

        public string Name
        {
            get { return corruption.name;  }
        }

        public bool Active {
            get { return active; }
        }

        internal void SetUp()
        {
            active = true;
            corruption.SetUp();
        }

        internal void TearDown()
        {
            active = false;
            corruption.TearDown();
        }
    }
}
