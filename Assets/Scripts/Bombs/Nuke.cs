using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : Bomb {

    protected override void Update()
    {
        base.Update();
        if (GetComponent<Rigidbody>() != null)
        {
            transform.forward = Vector3.Normalize(GetComponent<Rigidbody>().velocity);
        }
    }

    protected override void explode(){
		Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 999f);
		foreach (Collider c in collidersNearby)
		{
			if (c.gameObject.layer == 10)
            {
                GameObject e = Instantiate(explosion, c.transform.position, c.transform.rotation);
                e.transform.localScale = Vector3.one;
                Destroy (c.gameObject);
			}
		}
		Destroy(gameObject);
	}

}
