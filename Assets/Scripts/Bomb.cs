using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in collidersNearby){
            if (c.gameObject.layer == 9)
            {
                c.GetComponent<Player>().GetHit();
                c.GetComponent<Rigidbody>().AddExplosionForce(800f, transform.position, 50f, 5f);
            }
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
