using UnityEngine;

namespace ProjectFTP.Sound
{
    public class SoundsEffects : MonoBehaviour
    {
        public AudioClip jump;
        public AudioClip attack;
        public AudioClip damageTaken;

        public AudioSource audioSource;

        public void Start()
        {
            audioSource = GetComponent<AudioSource>();
            GetComponent<Character>().ActionHandler += OnCharacterAction;
        }    
    
        private void PlayClip(AudioClip clip)
        {
            if (clip)
            {
                audioSource.PlayOneShot(clip, GameManager.Instance.Profile.Settings.SoundEffectsVolume);
            }
        }

        private void OnCharacterAction(Character.Action action)
        {
            switch (action)
            {
                case Character.Action.JUMP:
                    PlayClip(jump);
                    break;
                case Character.Action.ATTACK:
                    PlayClip(attack);
                    break;
                case Character.Action.TAKE_DAMAGE:
                    PlayClip(damageTaken);
                    break;
            }
        }
    }
}
