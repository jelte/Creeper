using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public GameObject Target;
    public GameObject Target2;
    public GameObject ShootFromHere;
    public GameObject ShootFromHere2;
    public GameObject BallToSpawn;

    void Start()
    {
        InvokeRepeating("Fire", 1, 2); //after 1 second fire every 5
    }

    private void Update()
    {
        //check player position
        float Range = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, gameObject.transform.position);
        if (Range < 200)
        {
            Target = GameObject.FindWithTag("Player");
            Target2 = GameObject.FindWithTag("Player");
        }
        else
        {
            Target = ShootFromHere;
            Target2 = ShootFromHere2;
        }
    }

    void Fire()
    {
        //fire here
        GameObject BallInstance = Instantiate(BallToSpawn, ShootFromHere.transform.position, Quaternion.identity);
        Vector3 shoot = (Target.transform.position - BallInstance.transform.position).normalized;
        BallInstance.GetComponent<Rigidbody2D>().AddForce(shoot * 500.0f);

        GameObject BallInstance2 = Instantiate(BallToSpawn, ShootFromHere2.transform.position, Quaternion.identity);
        Vector3 shoot2 = (Target2.transform.position - BallInstance2.transform.position).normalized;
        BallInstance2.GetComponent<Rigidbody2D>().AddForce(shoot2 * 500.0f);
    }

    
}
