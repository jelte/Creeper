using UnityEngine;
using NUnit.Framework;
using ProjectFTP.Player;

namespace ProjectFTP.Tests.Editor {
	public class CharacterTest {

		GameObject gameObject;
		Character character;
		Rigidbody2D rb2d;

		[SetUp]
		public void SetUp() {
			gameObject = new GameObject ();
			rb2d = gameObject.AddComponent<Rigidbody2D> ();
			character = gameObject.AddComponent<Character> ();
		}

		[Test]
		public void Is_Alive() {
			Assert.True (character.IsAlive);
		}

		[Test]
		public void Is_Climbing() {
			Assert.False (character.IsClimbing);
		}

		[Test]
		public void Can_Attack() {
			character.Attack ();
		}

        [Test]
        public void Can_Take_Damage()
        {
            Assert.True(character.IsAlive);
            character.TakeDamage(1);
            Assert.False(character.IsAlive);
        }

		[Test]
		public void Can_Determine_Direction()
		{
			Assert.AreEqual(Direction.EAST, character.Facing);
			character.Move(Vector2.left);
			Assert.AreEqual(Direction.WEST, character.Facing);
		}
	}
}
