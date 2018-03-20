using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ProjectFTP.Player;

namespace ProjectFTP.UI
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public float sensitivity = 1.5f;

        private Character character;
        private Image bgImg;
        private Image JoystickImg;
        private Vector3 inputVector;
                
        void Start()
        {
            character = GameObject.FindObjectOfType<Character>();
            bgImg = GetComponent<Image>();
            JoystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        void Update()
        {
            // Move the character
            character.Move((Vector2)inputVector * sensitivity * Time.deltaTime);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            // Check if it is this component that is dragged and how far the draggin is.
            Vector2 pos;
            if (
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    bgImg.rectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out pos
                )
            ) {
                // Determine the draggin distance.
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
                
                inputVector = new Vector3(pos.x * 2 - 1, pos.y * 2 - 1, 0);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                // Update the visual representation.
                JoystickImg.rectTransform.anchoredPosition = new Vector3(
                    inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                    inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3)
                );
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Start Dragging.
            OnDrag(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            // Reset the movement.
            inputVector = Vector3.zero;
            JoystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}