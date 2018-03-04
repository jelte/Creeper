using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Progression
{
    [Serializable]
    public class Level
    {
        private int world;
        private int zone;
        private List<Attempt> attempts = new List<Attempt>();
        private bool current;

        public Level(int world, int zone)
        {
            this.world = world;
            this.zone = zone;
        }

        public Attempt Attempt
        {
            get
            {
                Attempt attempt = new Progression.Attempt();
                attempts.Add(attempt);
                return attempt;
            }
        }

        public int World
        {
            get { return world; }
        }

        public int Zone
        {
            get { return zone; }
        }

        public bool Current
        {
            get { return current; }
            set { current = value; }
        }

        public bool Completed
        {
            get { return attempts.Any(delegate (Attempt attempt) { return attempt.Success; }); }
        }

        public float TimeTaken {
            get {
                List<Attempt> completions = attempts.FindAll(delegate (Attempt attempt)
                {
                    return attempt.Success;
                });
                int firstIndex = -1;
                int lastIndex = attempts.IndexOf(completions[completions.Count - 1]);
                if (completions.Count > 1)
                {
                    firstIndex = attempts.IndexOf(completions[completions.Count - 2]);
                }
                return attempts.GetRange(firstIndex + 1, lastIndex - firstIndex).Sum(delegate(Attempt attempt) {
                    return attempt.Time;
                });
            }
        }

        public int Attempts {
            get {
                List<Attempt> completions = attempts.FindAll(delegate (Attempt attempt)
                {
                    return attempt.Success;
                });
                int firstIndex = 0;
                int lastIndex = attempts.IndexOf(completions[completions.Count - 1]);
                if (completions.Count > 1)
                {
                    firstIndex = attempts.IndexOf(completions[completions.Count - 2]);
                }
                return lastIndex - firstIndex;
            }
        }
    }
}
