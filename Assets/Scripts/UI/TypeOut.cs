using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

namespace ProjectFTP.UI
{
    public class TypeOut : MonoBehaviour
    {
        [TextArea]
        public String finalText;
        public float RandomCharacterChangeRate = 0.1f;
        public float TypeRate = 0.2f;

        private string currentRandomCharacter;
        private int i = -1;
        private Text text;

        public void AnimateText(string value)
        {
            if (i == -1 || text.text.Length == finalText.Length)
            {
                finalText = value;
                text.text = "";
                i = 0;
                enabled = true;
            } else
            {
                finalText += value;
            }
            CancelInvoke("Hide");
            InvokeRepeating("RandomizeCharacter", 0.0f, RandomCharacterChangeRate);
            InvokeRepeating("NextCharacter", TypeRate, TypeRate);
        }

        void Start()
        {
            text = GetComponent<Text>();
        }

        private void RandomizeCharacter()
        {
            byte value = (byte)UnityEngine.Random.Range(41f, 128f);

            currentRandomCharacter = Encoding.ASCII.GetString(new byte[] { value });
        }

        private void NextCharacter()
        {
            i++;
            while (i < finalText.Length && char.IsWhiteSpace(finalText[i]))
            {
                i++;
            }
        }

        void OnGUI()
        {
            if (i == -1)
            {
                return;
            }

            text.text = finalText.Substring(0, i) + currentRandomCharacter;

            if (text.text.Length == finalText.Length + 1)
            {
                text.text = finalText;
                enabled = false;
                CancelInvoke("RandomizeCharacter");
                CancelInvoke("TypeOut");
                Invoke("Hide", 2.0f);
            }
        }

        void Hide()
        {
            gameObject.GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
