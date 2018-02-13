using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Level {
    public class DestroyBall : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 3.0f);
        }
    }
}
