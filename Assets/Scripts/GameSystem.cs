using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

	public GameObject allyPlayerPrefab;
	public GameObject axisPlayerPrefab;
	private GameObject allyPlayerObject;
	private GameObject axisPlayerObject;
	private Player allyPlayer;
	private Player axisPlayer;
	private int minimumHeight = 0;
	public Vector3 allyPlayerStartPosition;
	public Vector3 axisPlayerStartPosition;

	// Use this for initialization
	void Start () {
		allyPlayerObject = Instantiate (allyPlayerPrefab);
		axisPlayerObject = Instantiate (axisPlayerPrefab);
		allyPlayer = allyPlayerObject.GetComponent<Player> ();
		axisPlayer = axisPlayerObject.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Check if any Player fell off
		if (allyPlayerObject.transform.position.y < minimumHeight){
			fellOff (allyPlayerObject);
		}
		if (axisPlayerObject.transform.position.y < minimumHeight){
			fellOff (axisPlayerObject);
		}

		//Ally Player Movement
		if (Input.GetKey (KeyCode.W)) {
			allyPlayer.moveUp();
		}
		if (Input.GetKey (KeyCode.A)) {
			allyPlayer.moveLeft();
		}
		if (Input.GetKey (KeyCode.S)) {
			allyPlayer.moveRight();
		}
		if (Input.GetKey (KeyCode.D)) {
			allyPlayer.moveDown();
		}
		if (Input.GetKey (KeyCode.E)) {
			allyPlayer.grabBomb();
		}
		if (Input.GetKey (KeyCode.R)) {
			allyPlayer.jump();
		}

		//Axis Player Movement
		if (Input.GetKey (KeyCode.UpArrow)) {
			axisPlayer.moveUp();
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			axisPlayer.moveLeft();
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			axisPlayer.moveRight();
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			axisPlayer.moveDown();
		}
		if (Input.GetKey (KeyCode.Period)) {
			allyPlayer.grabBomb();
		}
		if (Input.GetKey (KeyCode.Slash)) {
			allyPlayer.jump ();
		}

	}
		
	private void fellOff(GameObject player){

	}

	private void winGame(GameObject player){

	}

}
