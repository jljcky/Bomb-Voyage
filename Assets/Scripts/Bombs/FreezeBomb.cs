using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBomb : Bomb {

	protected override void explode(){
		Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 8f * bombCharge);
		foreach (Collider c in collidersNearby)
		{
			if (c.gameObject.layer == 9)
			{
				c.GetComponent<Player> ().isStunned = true;
				c.GetComponent<Player> ().setStunnedcd (-5f);
			}
		}
		GameObject e = Instantiate(explosion, transform.position, transform.rotation);
		e.transform.localScale = Vector3.one * bombCharge;
		Destroy(gameObject);
	}

}
