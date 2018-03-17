using UnityEngine;

namespace ProjectFTP.Sound
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic instance;
        private AudioSource audioSource;
        public AudioClip[] audioClips;
        
        void Start()
        {
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

        public void UpdateVolume()
        {
            audioSource.volume = GameManager.BackgroundMusicVolume;
        }

  
        public void Restore()
        {
            audioSource.pitch = 1f;
            UpdateVolume();
        }

        public void AdjustPitch(float pitch)
        {
            audioSource.pitch = pitch;
        }

        public static BackgroundMusic Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new System.Exception("GameManager not instanciated.");
                }
                return instance;
            }
        }
    }
}