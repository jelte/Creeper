using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.UI
{
    public class FollowPlayer : MonoBehaviour
    {
        GameObject character;
        public Vector3 offset;
        public float maxDistance = 10f;

        public void StartFollow(GameObject character, Vector3 offset)
        {
            this.character = character;
            this.offset = offset;
        }
        
        void Update()
        {
            if (character != null)
            {
                float magnitude = (transform.position - (character.transform.position + offset)).magnitude / 10f;
                transform.position = Vector3.Lerp(
                    transform.position,
                    character.transform.position + offset,
                    Mathf.Clamp(magnitude, 0f, 1f)
                );
            }
        }
    }
}