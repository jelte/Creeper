using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CharacterPhyisicsTest {

	GameObject gameObject;
	Character character;
	Rigidbody2D rb2d;

	[SetUp]
	public void SetUp() {
		gameObject = new GameObject ();
		gameObject.AddComponent<CapsuleCollider2D> ();
		rb2d = gameObject.AddComponent<Rigidbody2D> ();
		character = gameObject.AddComponent<Character> ();
		Physics2D.gravity = Vector2.zero;
	}

	[TearDown]
	public void TearDown() {
		Physics2D.gravity = Vector2.down * 9.8f;
	}

	[UnityTest]
	public IEnumerator Is_Subjected_To_Forces() {
		rb2d.AddForce(Vector2.left * 100);
		yield return null;
		Assert.AreEqual (new Vector2(-2f, 0f), rb2d.velocity);
	}

	[UnityTest]
	public IEnumerator Force_Is_Negated_When_Moving_Opposite() {
		rb2d.AddForce(Vector2.left * 100);
		yield return null;
		Assert.AreEqual (new Vector2(-2f, 0f), rb2d.velocity);
		yield return null;
		character.Move (Vector2.left);
		yield return null;
		Assert.Less (rb2d.velocity.x, -2.5f);
	}
}
