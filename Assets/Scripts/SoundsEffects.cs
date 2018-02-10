using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffects : MonoBehaviour {
	
    public AudioClip jump;
    public AudioClip ballCollision;
    public AudioClip boundCollision;

    public GameManager gm;

    public void Update()
    {
        if (gm == null)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    private void MakeSound(AudioClip originalClip)
    {
        GetComponent<AudioSource>().volume = gm.soundEfeectsVolume;
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
