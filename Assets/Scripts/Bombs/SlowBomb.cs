using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBomb : Bomb {

	protected override void explode(){
		Destroy(gameObject);
	}

}
