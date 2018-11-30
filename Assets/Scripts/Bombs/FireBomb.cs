using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Bomb {

	protected override void explode(){
		Destroy(gameObject);
	}

}
