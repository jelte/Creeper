using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Player
{
    public class LevelStats
    {
        private List<Attempt> list;
        private float time = 0.0f;

        public LevelStats(List<Attempt> list)
        {
            int lastSuccess = list.GetRange(0, list.Count - 1).FindLastIndex(delegate (Attempt attempt) { return attempt.Success; });
            this.list = lastSuccess == -1 ? list : list.GetRange(lastSuccess + 1, list.Count - (lastSuccess + 1));
            Attempts = this.list.Count;
            
            this.list.ForEach(delegate (Attempt attempt) {
                time += attempt.Time;
            });
        }

        public int Attempts { get; internal set; }
        public float Minutes {
            get { return Mathf.Floor(time / 60); }
        }
        public float Seconds {
            get { return Mathf.Round(time % 60 * 1000)/1000; }
        }
    }
}