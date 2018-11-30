using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : Bomb {

	protected override void explode(){
		Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 999f);
		foreach (Collider c in collidersNearby)
		{
			if (c.gameObject.layer == 10)
			{
				Destroy (c.gameObject);
			}
		}
		GameObject e = Instantiate(explosion, transform.position, transform.rotation);
		e.transform.localScale = Vector3.one * 99f;
		Destroy(gameObject);
	}

}
