using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Bomb {

	protected override void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 9) {
			explode ();
		} 
		else if (other.gameObject.layer == 10){
			GameObject newMine = Instantiate (this.gameObject);
			Destroy (newMine.GetComponent<Rigidbody>());
			Destroy (gameObject);
		}
		else {
			explode();
		}
	}

	protected override void explode(){
		Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 8f * bombCharge);
		foreach (Collider c in collidersNearby)
		{
			if (c.gameObject.layer == 9)
			{
				c.GetComponent<Player>().GetHit(bombCharge, transform.position);
			}
		}
		GameObject e = Instantiate(explosion, transform.position, transform.rotation);
		e.transform.localScale = Vector3.one * bombCharge;
		Destroy(gameObject);
	}
}
