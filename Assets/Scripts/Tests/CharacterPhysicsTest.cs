using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System.Collections;
using System.Collections.Generic;
using ProjectFTP.Player;

namespace ProjectFTP.Tests {
    [TestFixture]
	public class CharacterPhyisicsTest {

        class Vector2DComparer : IEqualityComparer<Vector2>
        {
            public float margin = 0.002f;

            public Vector2DComparer(float margin)
            {
                this.margin = margin;
            }

            public bool Equals(Vector2 a, Vector2 b)
            {
                return Mathf.Abs(a.x - b.x) <= margin && Mathf.Abs(a.y - b.y) <= margin;
            }

            public int GetHashCode(Vector2 obj)
            {
                throw new System.NotImplementedException();
            }
        }
        class FloatComparer : IEqualityComparer<float>
        {
            public float margin = 0.002f;

            public bool Equals(float a, float b)
            {
                return Mathf.Abs(a - b) <= margin;
            }

            public int GetHashCode(float obj)
            {
                throw new System.NotImplementedException();
            }
        }
        
    
        GameObject ground;
        GameObject ladder;
  
        Vector2DComparer vector2DComparer = new Vector2DComparer(0.002f);
        FloatComparer floatComparer = new FloatComparer();

        private Character CreateCharacter()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "player";
            gameObject.layer = LayerMask.NameToLayer("Player");
            gameObject.AddComponent<CapsuleCollider2D>();
            Rigidbody2D rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            BoxCollider2D characterCollider = gameObject.AddComponent<BoxCollider2D>();
            characterCollider.size = new Vector2(2f, 2f);
            return gameObject.AddComponent<Character>();
        }

        [SetUp]
		public void SetUp() {
            // Set up world

            // ground
            ground = new GameObject();
            ground.name = "ground";
            ground.layer = LayerMask.NameToLayer(Level.TileType.Ground.ToString());
            ground.transform.position = Vector2.down;
            BoxCollider2D groundCollider = ground.AddComponent<BoxCollider2D>();
            groundCollider.size = new Vector2(10f, 0.4f);
            groundCollider.offset = new Vector2(-5f, -0.2f);

            // Set up climbable surface
            ladder = new GameObject();
            ladder.name = "ladder";
            ladder.layer = LayerMask.NameToLayer(Level.TileType.Climbable.ToString());
            ladder.transform.position = Vector2.up * 10 + Vector2.right * 15;
            BoxCollider2D ladderCollider = ladder.AddComponent<BoxCollider2D>();
            ladderCollider.size = new Vector2(10f, 10f);
        }

		[TearDown]
		public void TearDown() {
			Physics2D.gravity = Vector2.down * 9.8f;
		}

		[UnityTest]
		public IEnumerator Force_Is_Negated_When_Moving_Opposite()
        {
            Character character = CreateCharacter();
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
            
            Assert.That(rb2d.velocity.x, Is.EqualTo(Vector2.zero.x).Using(new Vector2DComparer(0.025f)));
            rb2d.velocity = Vector2.left * 20f;
            Assert.That(rb2d.velocity.x, Is.EqualTo(-20f).Using(new Vector2DComparer(0.025f)));
      
			character.Move (Vector2.right * Time.deltaTime);
			yield return null;
            Assert.That(rb2d.velocity, Is.EqualTo(Vector2.left * 19f).Using(new Vector2DComparer(0.5f)));
            character.Move(Vector2.right * Time.deltaTime);
            yield return null;
            Assert.That(rb2d.velocity, Is.EqualTo(Vector2.left * 18.25f).Using(new Vector2DComparer(0.5f)));
        }

        [UnityTest]
        public IEnumerator Can_Move_Left()
        {
            Character character = CreateCharacter();
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
            character.Move(Vector2.left);
            yield return null;
            Assert.That(rb2d.position, Is.EqualTo(Vector2.left * 5f).Using(new Vector2DComparer(0.1f)));
        }

        [UnityTest]
        public IEnumerator Can_Move_Right()
        {
            Character character = CreateCharacter();
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
            character.Move(Vector2.right);
            yield return null;
            Assert.That(rb2d.position, Is.EqualTo(Vector2.right * 5f).Using(new Vector2DComparer(0.1f)));
        }


        [UnityTest]
        public IEnumerator Can_Climb()
        {
            Character character = CreateCharacter();
            Vector2 start = new Vector2(15f, 10f);
            character.transform.position = start;

            Assert.False(character.IsClimbing);
            character.Climb();
            Assert.True(character.IsClimbing);

            yield return new WaitForFixedUpdate();
            Assert.True(character.IsClimbing);
        }


        [UnityTest]
        public IEnumerator Can_Climb_Up()
        {
            Character character = CreateCharacter();
            Vector2 start = new Vector2(15f, 10f);
            character.transform.position = start;
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();
            
            character.Climb();
            Assert.AreEqual(0.0f, rb2d.gravityScale);
            Assert.True(character.IsClimbing);
            character.Move(Vector2.up);
            yield return new WaitForFixedUpdate();
            Assert.True(character.IsClimbing);
            // TODO Validate position
            //Assert.That(rb2d.position, Is.EqualTo(start + (Vector2.up * 2.5f)).Using(new Vector2DComparer(0.025f)));
        }

        [UnityTest]
        public IEnumerator Can_Climb_Down()
        {
            Character character = CreateCharacter();
            Vector2 start = new Vector2(15f, 10f);
            character.transform.position = start;
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();

            character.Climb();
            Assert.AreEqual(0.0f, rb2d.gravityScale);
            Assert.True(character.IsClimbing);
            character.Move(Vector2.down);
            yield return new WaitForFixedUpdate();
            Assert.True(character.IsClimbing);
            // TODO Validate position
           // Assert.That(rb2d.position, Is.EqualTo(start + (Vector2.down * 2.5f)).Using(new Vector2DComparer(0.025f)));
        }

        [UnityTest]
        public IEnumerator Can_Jump()
        {
            Character character = CreateCharacter();
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();

            Assert.True(character.IsGrounded);
            character.Jump();

            Assert.AreEqual(-Physics2D.gravity * 1.8f, rb2d.velocity);
            yield return new WaitForFixedUpdate();
            Assert.False(character.IsGrounded);
        }

        [UnityTest]
        public IEnumerator Can_Double_Jump()
        {
            Character character = CreateCharacter();
            Rigidbody2D rb2d = character.GetComponent<Rigidbody2D>();

            Assert.True(character.IsGrounded);
            character.Jump();
            Assert.AreEqual(-Physics2D.gravity * 1.8f, rb2d.velocity);
            yield return new WaitForFixedUpdate();
            Assert.False(character.IsGrounded);
            character.Jump();
            Assert.AreEqual(-Physics2D.gravity * 1.8f, rb2d.velocity);
        }

        [UnityTest]
        public IEnumerator Is_Grounded()
        {
            Character character = CreateCharacter();

            yield return null;
            Assert.True(character.IsGrounded);
        }
    }
}
