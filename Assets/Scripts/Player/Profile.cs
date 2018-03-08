using ProjectFTP.Level;
using System;
using System.Collections.Generic;

namespace ProjectFTP.Player
{
    [Serializable]
    public class Profile
    {
        private Dictionary<string, Dictionary<string, List<Attempt>>> progression = new Dictionary<string, Dictionary<string, List<Attempt>>>();

        public Profile()
        {
            CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; internal set; }
        public DateTime LastUpdatedOn { get; internal set; }
        public bool Active { get; set; }

        public void AddAttempt(Attempt attempt)
        {
            // Init world
            Dictionary<string, List<Attempt>> world;
            if (!progression.TryGetValue(attempt.World, out world))
            {
                world = new Dictionary<string, List<Attempt>>();
                progression.Add(attempt.World, world);
            }

            // Init level
            List<Attempt> level;
            if (!world.TryGetValue(attempt.Level, out level))
            {
                level = new List<Attempt>();
                world.Add(attempt.Level, level);
            }
            
            // Check if attempt hasn't been registered yet
            if (!level.Contains(attempt))
            {
                // if not add attempt
                level.Add(attempt);
            }
            LastUpdatedOn = DateTime.Now;
        }

        public LevelStats GetLevelStats(WorldConfig world, LevelConfig level)
        {
            // Check if level exists in progress
            List<Attempt> levelProgress = GetLevelProgress(world, level);
            if (levelProgress == null)
            {
                levelProgress = new List<Attempt>();
            }

            return new LevelStats(GetLevelProgress(world, level));
        }

        public bool Completed(WorldConfig world, LevelConfig level)
        {
            // Check if level exists in progress
            List<Attempt> levelProgress = GetLevelProgress(world, level);
            if (levelProgress == null)
            {
                return false;
            }

            // Check if level has been completed
            return levelProgress.Find(delegate (Attempt attempt) { return attempt.Success; }) != null;
        }

        private List<Attempt> GetLevelProgress(WorldConfig world, LevelConfig level)
        {
            // Check if world exists in progress
            Dictionary<string, List<Attempt>> worldProgress;
            if (!progression.TryGetValue(world.name, out worldProgress))
            {
                return null;
            }

            // Check if level exists in progress
            List<Attempt> levelProgress;
            if (!worldProgress.TryGetValue(level.name, out levelProgress))
            {
                return null;
            }

            return levelProgress;
        }
    }
}
