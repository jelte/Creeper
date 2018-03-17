using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ProjectFTP.UI
{
    public class VirturalJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        private Image bgImg;
        private Image JoystickImg;
        private Vector3 inputVector;
        private Character character;
        
        float rot;
        float c_rot;
        
        private void Start()
        {
            character = GameObject.FindObjectOfType<Character>();
            bgImg = GetComponent<Image>();
            JoystickImg = transform.GetChild(0).GetComponent<Image>();
            rot = 0f;
        }

        public virtual void OnDrag(PointerEventData ped)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out pos))
            {
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
                
                inputVector = new Vector3(pos.x * 2 - 1, pos.y * 2 - 1, 0);

                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                JoystickImg.rectTransform.anchoredPosition =
                    new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
                        inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3));

                character.Move((Vector2)inputVector * Time.deltaTime);
            }
        }

        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped);
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            inputVector = Vector3.zero;
            JoystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }

        public float Horizontal()
        {
            if (inputVector.x != 0)
                return inputVector.x;
            else
                return Input.GetAxis("Horizontal");
        }

        public float Vertical()
        {
            if (inputVector.z != 0)
                return inputVector.z;
            else
                return Input.GetAxis("Vertical");
        }

        public float MouseX()
        {
            if (inputVector.x != 0)
                rot += inputVector.x;
            return rot;
        }

        public float MouseY()
        {
            if (inputVector.x != 0)
                c_rot -= inputVector.z * 0.5f;
            return c_rot;
        }
    }
}