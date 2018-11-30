using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBomb : Bomb {

	protected override void explode(){
		Destroy(gameObject);
	}

}
