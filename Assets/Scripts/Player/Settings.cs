using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFTP.Player
{
    [Serializable]
    public class Settings
    {
        private float backgroundMusicVolume;
        private float soundEffectsVolume;

        public Settings() : this(1f, 1f)
        {

        }

        public Settings(float backgroundMusicVolume, float soundEffectsVolume)
        {
            this.backgroundMusicVolume = backgroundMusicVolume;
            this.soundEffectsVolume = soundEffectsVolume;
        }

        public float SoundEffectsVolume
        {
            get { return soundEffectsVolume; }
            set { soundEffectsVolume = value; }
        }

        public float BackgroundMusicVolume
        {
            get { return backgroundMusicVolume; }
            set { backgroundMusicVolume = value; }
        }
    }
}
