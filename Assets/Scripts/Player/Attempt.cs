using ProjectFTP.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Player
{
    [Serializable]
    public class Attempt
    {
        private bool finished = false;
        
        public Attempt(WorldConfig world, LevelConfig level)
        {
            World = world.name;
            Level = level.name;
        }

        public void Tick(float deltaTime)
        {
            if (finished)
            {
                return;
            }
            Time += deltaTime;
        }

        private void Finish(bool success)
        {
            if (finished)
            {
                return;
            }
            finished = true;
            Success = success;
        }

        public void Victory()
        {
            Finish(true);
        }

        public void Dead()
        {
            Finish(false);
        }

        public float Time { get; internal set; }
        public bool Success { get; internal set; }
        public string World { get; internal set; }
        public string Level { get; internal set; }
    }
}
