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

        // Use this for initialization
        void Start()
        {
            instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        public void Restore()
        {
            audioSource.volume = GameManager.Instance.Profile.Settings.BackgroundMusicVolume;
            audioSource.pitch = 1f;
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