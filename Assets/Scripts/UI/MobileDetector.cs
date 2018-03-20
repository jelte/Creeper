using UnityEngine;

namespace ProjectFTP.UI
{
    public class MobileDetector : MonoBehaviour
    {
        void Start()
        {
#if UNITY_IPHONE
#elif UNITY_ANDROID
#else
            gameObject.active = false;
#endif
        }
    }
}
