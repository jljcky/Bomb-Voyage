using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBomb : Bomb {

	protected override void explode(){
		Destroy(gameObject);
	}

}
