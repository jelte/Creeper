using ProjectFTP.Player;
using UnityEngine;

namespace ProjectFTP.Corruptions
{
    [CreateAssetMenu(menuName = "Corruptions/MovementSpeed")]
    public class MovementSpeedCorruption : Corruption
    {
        public float modifier = 1.0f;

        public override void SetUp()
        {
            Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            character.runSpeed *= modifier;
            character.climbSpeed *= modifier;
        }

        public override void TearDown()
        {
            Character character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
            character.runSpeed /= modifier;
            character.climbSpeed /= modifier;
        }
    }
}