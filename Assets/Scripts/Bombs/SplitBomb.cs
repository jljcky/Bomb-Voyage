using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBomb : Bomb {
	private float lift = 15f;
	private float thrust = 5f;
	private float seperationDistance = 2f;
	private float upDistance = 2f;

	public GameObject normalBombPrefab;

	protected override void explode(){
		Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 4f * bombCharge);
		foreach (Collider c in collidersNearby)
		{
			if (c.gameObject.layer == 9)
			{
				c.GetComponent<Player>().GetHit(bombCharge, transform.position);
			}
		}
		GameObject e = Instantiate(explosion, transform.position, transform.rotation);
		e.transform.localScale = (Vector3.one * bombCharge) / 2f;
		GameObject split1 = Instantiate (normalBombPrefab);
		makeBomb (split1);

		GameObject split2 = Instantiate (normalBombPrefab);
		makeBomb (split2);

		GameObject split3 = Instantiate (normalBombPrefab);
		makeBomb (split3);

		GameObject split4 = Instantiate (normalBombPrefab);
		makeBomb (split4);

		split1.transform.position = transform.position + transform.forward * seperationDistance + transform.up * upDistance; 
		split2.transform.position = transform.position + -transform.forward * seperationDistance + transform.up * upDistance; 
		split3.transform.position = transform.position + transform.right * seperationDistance + transform.up * upDistance;  
		split4.transform.position = transform.position + -transform.right * seperationDistance + transform.up * upDistance; 

		split1.GetComponent<Rigidbody> ().AddForce (transform.forward * thrust  + Vector3.up * lift, ForceMode.VelocityChange);
		split2.GetComponent<Rigidbody> ().AddForce (-transform.forward * thrust  + Vector3.up * lift, ForceMode.VelocityChange);
		split3.GetComponent<Rigidbody> ().AddForce (transform.right * thrust + Vector3.up * lift, ForceMode.VelocityChange);
		split4.GetComponent<Rigidbody> ().AddForce (-transform.right * thrust + Vector3.up * lift, ForceMode.VelocityChange);
		Destroy(gameObject);
	}

	private void makeBomb(GameObject bomb){
		bomb.GetComponent<Collider> ().isTrigger = true;
		bomb.AddComponent<Rigidbody> ();
		bomb.GetComponent<Rigidbody> ().useGravity = true;
		bomb.GetComponent<Bomb> ().setThrown ();
	}

}
