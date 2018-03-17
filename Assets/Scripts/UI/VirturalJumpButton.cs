using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectFTP.UI
{
    public class VirturalJumpButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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
            character.Jump();
            image.color = new Color(255, 255, 255, 50);
        }
        public virtual void OnPointerUp(PointerEventData ped)
        {
            image.color = new Color(255, 255, 255, 150);
        }
    }
}