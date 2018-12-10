using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBomb : Bomb {
	protected override void Update()
	{
		base.Update();
	}

	protected override void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.layer == 10){
			explode ();
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

		GameObject newCube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newCube.transform.position = transform.position;
		newCube.transform.rotation = transform.rotation;
		newCube.transform.localScale = Vector3.one * 5F * bombCharge;
		newCube.transform.position = transform.position + new Vector3 (0, (newCube.transform.localScale.y / 2f) - 1, 0);
		newCube.gameObject.layer = 10;
		Destroy(gameObject);
	}
}

