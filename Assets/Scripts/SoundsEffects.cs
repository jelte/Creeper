using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffects : MonoBehaviour {
	
    public AudioClip jump;
    public AudioClip ballCollision;
    public AudioClip boundCollision;

    // Use this for initialization

    private void MakeSound(AudioClip originalClip)
    {
        GetComponent<AudioSource>().volume = 0.8f;
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }               

    public void MakeJumpSound()
    {
        MakeSound(jump);
    }

    public void MakeBallCollisionSound()
    {
        MakeSound(ballCollision);
    }
    public void MakeBoundCollisionSound()
    {
        MakeSound(boundCollision);
    }
}
