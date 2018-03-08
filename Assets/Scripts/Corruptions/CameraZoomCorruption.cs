using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProjectFTP.Corruptions
{
    [CreateAssetMenu(menuName = "Corruptions/CameraZoom")]
    class CameraZoomCorruption : Corruption
    {
        public float modifier = 1.0f;

        public override void SetUp()
        {
            Camera.main.orthographicSize *= modifier;
        }

        public override void TearDown()
        {
            Camera.main.orthographicSize /= modifier;
        }
    }
}
