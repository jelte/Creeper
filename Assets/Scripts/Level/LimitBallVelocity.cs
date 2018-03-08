using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBallVelocity : MonoBehaviour {

    private Rigidbody2D rg;
    public float MaxballSpeed;

    private void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        MaxballSpeed = 20.0f;
    }


    void FixedUpdate()
    {
        if (Mathf.Abs(rg.velocity.x) > MaxballSpeed || Mathf.Abs(rg.velocity.y) > MaxballSpeed)
        {
            // clamp velocity:
            Vector3 newVelocity = rg.velocity.normalized;
            newVelocity *= MaxballSpeed;
            rg.velocity = newVelocity;
        }
    }
}
