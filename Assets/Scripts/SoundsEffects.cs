using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsEffects : MonoBehaviour {

    public GameObject player;
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
   
    // Update is called once per frame
    void Update () {
        if (player == null && GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = player.transform.position;
    }
}
