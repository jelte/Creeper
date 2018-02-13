using System;
using System.Collections.Generic;

namespace ProjectFTP.Player
{
    [Serializable]
    public class Profile
    {
        private DateTime createdOn;
        private DateTime lastUpdatedOn;
        private Settings settings;
        private bool active = false;
        private List<Progression.Level> storyModeLevels = new List<Progression.Level>();

        public Profile()
        {
            this.settings = new Settings(1f, 1f);
            this.createdOn = DateTime.Now;
        }

        public DateTime CreateOn
        {
            get { return createdOn;  }
        }

        public DateTime LastUpdatedOn
        {
            get { return lastUpdatedOn;  }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Progression.Level CurrentStoryModeLevel
        {
            get {
                Progression.Level current = storyModeLevels.Find(delegate (Progression.Level level) { return level.Current; });
                if (current == null)
                {
                    current = new Progression.Level(1, 1);
                    storyModeLevels.Add(current);
                }
                return current;
            }
        }

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }
    }
}
