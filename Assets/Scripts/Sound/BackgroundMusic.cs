using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Sound
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic instance;
        
        private AudioSource audioSource;

        public AudioClip audioClip;
        public AudioClip audioClip2;

        // Use this for initialization
        void Start()
        {
            int random = Random.Range(0, 2);
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            if (random == 0)
            {
                audioSource.clip = audioClip;
            }
            else
            {
                audioSource.clip = audioClip2;
            }
                
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