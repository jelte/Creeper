using UnityEngine;

namespace ProjectFTP.Player
{
    public class SoundsEffects : MonoBehaviour
    {
        public AudioClip jump;
        public AudioClip attack;
        public AudioClip damageTaken;

        private AudioSource audioSource;

        public void Start()
        {
            audioSource = GetComponent<AudioSource>();
            // Listing to character events
            GetComponent<Character>().ActionHandler += OnCharacterAction;
        }    
    
        private void PlayClip(AudioClip clip)
        {
            if (clip)
            {
                // play the sound clip at the current location
                audioSource.PlayOneShot(clip, GameManager.SoundEffectsVolume);
            }
        }

        private void OnCharacterAction(Character.Action action)
        {
            // Depending on action, play the corresponding audio clip
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
