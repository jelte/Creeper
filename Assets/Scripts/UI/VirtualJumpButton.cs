using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectFTP.Player;

namespace ProjectFTP.UI
{
    public class VirtualJumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private Character character;
        private Image image;

        void Start()
        {
            character = GameObject.FindObjectOfType<Character>();
            image = gameObject.GetComponent<Image>();
        }

        public virtual void OnPointerDown(PointerEventData ped)
        {
            // initiate the character to jump
            character.Jump();
            // change the color of the button
            image.color = new Color(255, 255, 255, 50);
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            // Restore the color
            image.color = new Color(255, 255, 255, 150);
        }
    }
}