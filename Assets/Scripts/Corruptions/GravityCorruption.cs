using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectFTP.Corruptions
{
    [CreateAssetMenu(menuName = "Corruptions/Gravity")]
    public class GravityCorruption : Corruption
    {
        public float modifier;

        public override void SetUp()
        {
            Physics2D.gravity *= modifier;
        }

        public override void TearDown()
        {
            Physics2D.gravity /= modifier;
        }
    }
}
