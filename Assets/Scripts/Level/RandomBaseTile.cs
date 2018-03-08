using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBaseTile : MonoBehaviour {


    public Sprite[] SpriteArray;
    public int RandomTile;

    // Use this for initialization
    void Start ()
    {
        RandomTile = Random.Range(1, 34);
        this.GetComponent<SpriteRenderer>().sprite = SpriteArray[RandomTile];
    }
	
}
