﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikeTrap : MonoBehaviour {

    public GameObject ObjectToMove;
    
     private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for player
        if(collision.tag == "Player")
        {

            ObjectToMove.GetComponent<MovignSpike>().MoveTowards = true;

        }
        
    }




}