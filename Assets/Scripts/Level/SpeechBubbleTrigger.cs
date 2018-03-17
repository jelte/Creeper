using ProjectFTP.UI;
using UnityEngine;

namespace ProjectFTP.Level
{
    public class SpeechBubbleTrigger : MonoBehaviour
    {
        public SpeechBubble speechBubble;

        private void Start()
        {
            transform.position = speechBubble.position;
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.radius = speechBubble.Radius;
            collider.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            TypeOut text = GameObject.FindObjectOfType<TypeOut>();

            text.AnimateText(speechBubble.text);
            text.GetComponentInParent<Canvas>().enabled = true;
        }
    }
}

