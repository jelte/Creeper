using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Progression
{
    [Serializable]
    public class Attempt
    {
        private float time = 0.0f;
        private bool success = false;
        private bool finished = false;
        
        public void Tick(float deltaTime)
        {
            if (finished)
            {
                return;
            }
            time += deltaTime;
        }

        private void Finish(bool success)
        {
            if (finished)
            {
                return;
            }
            finished = true;
            this.success = success;
        }

        public void Victory()
        {
            Finish(true);
        }

        public void Dead()
        {
            Finish(false);
        }

        public float Time
        {
            get { return time;  }
        }

        public bool Success
        {
            get { return success;  }
        }
    }
}
