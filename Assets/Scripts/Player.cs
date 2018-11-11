using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int Health;
	private float bombCharge;
	private float maxCharge = 5;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		Health = 5;
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		//Increase Bomb Charges
		if (bombCharge > 0 && bombCharge < maxCharge)
			bombCharge += 1f * Time.deltaTime;	
	}

	//Grab Bomb, and if already holding then throw
	public bool grabBomb(){
		if (bombCharge != 0) {
			throwBomb ();
			return false;
		} else {
			bombCharge = 1;
			return true;
		}
	}
	private void throwBomb (){
		
	}

	//Movement
	public void moveUp(){

	}
	public void moveDown(){

	}
	public void moveLeft(){

	}
	public void moveRight(){

	}
	public void jump(){

	}

	public void getHit(){
		
	}

}
