using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

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
		Assert.True (character.Alive);
	}

	[Test]
	public void Has_Position() {
		Assert.AreEqual (Vector3.zero, gameObject.transform.position);
	}

	[Test]
	public void Is_Grounded() {
		Assert.True (character.Grounded);
	}

	[Test]
	public void Is_Climbing() {
		Assert.False (character.Climbing);
	}

	[Test]
	public void Can_Jump() {
		Assert.True (character.Grounded);
		character.Jump ();
		Assert.False (character.Grounded);
	}

	[Test]
	public void Can_Double_Jump() {
		Assert.True (character.Grounded);
		character.Jump ();
		Assert.False (character.Grounded);
		character.Jump ();
		Assert.AreEqual(Physics2D.gravity * -1.5f * character.jumpModifier, gameObject.GetComponent<Rigidbody2D>().velocity);
	}

	[Test]
	public void Can_Attack() {
		character.Attack ();
	}

	[Test]
	public void Can_Climb() {
		Assert.False (character.Climbing);
		character.Climb ();
		Assert.True (character.Climbing);
	}

	[Test]
	public void Can_Move_Left() {
		character.Move (Vector2.left);
		Assert.AreEqual(Vector2.left, gameObject.GetComponent<Rigidbody2D>().position);
	}

	[Test]
	public void Can_Move_Right() {
		character.Move (Vector2.right);
		Assert.AreEqual(Vector2.right, rb2d.position);
	}

	[Test]
	public void Can_Climb_Up()
	{
		character.Climb ();
		character.Move (Vector2.up);
		Assert.AreEqual(Vector2.up, rb2d.position);
	}

	[Test]
	public void Can_Climb_Down()
	{
		character.Climb ();
		character.Move (Vector2.down);
		Assert.AreEqual(Vector2.down, rb2d.position);
	}
}
