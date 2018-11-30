using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : Bomb {

	protected override void explode(){
		Destroy(gameObject);
	}

}
