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
        allyPlayerObject.transform.position = new Vector3(-1f, 1f, 0f);
        axisPlayerObject.transform.position = new Vector3(1f, 1f, 0f);
        allyPlayer = allyPlayerObject.GetComponent<Player> ();
		axisPlayer = axisPlayerObject.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Check if any Player fell off
		if (allyPlayerObject.transform.position.y < minimumHeight){
			FellOff (allyPlayerObject);
		}
		if (axisPlayerObject.transform.position.y < minimumHeight){
			FellOff (axisPlayerObject);
		}

        //Ally Player Movement
        allyPlayer.inputs = Vector2.zero;
		if (Input.GetKey (KeyCode.W)) {
            //allyPlayer.MoveUp();
            allyPlayer.inputs.y += 1f;
        }
		if (Input.GetKey (KeyCode.A)) {
            //allyPlayer.MoveLeft();
            allyPlayer.inputs.x -= 1f;
        }
		if (Input.GetKey (KeyCode.S)) {
            //allyPlayer.MoveRight();
            allyPlayer.inputs.y -= 1f;
        }
		if (Input.GetKey (KeyCode.D)) {
            //allyPlayer.MoveDown();
            allyPlayer.inputs.x += 1f;
        }
		if (Input.GetKey (KeyCode.E)) {
			allyPlayer.GrabBomb();
		}
		if (Input.GetKey (KeyCode.R)) {
			allyPlayer.Jump();
		}
        if (allyPlayer.inputs != Vector2.zero && !allyPlayer.isStunned)
        {
            allyPlayer.Move();
            allyPlayer.GetComponent<Animator>().Play("Walk");
        }
        else{
            allyPlayer.GetComponent<Animator>().Play("Idle");
        }

        //Axis Player Movement
        axisPlayer.inputs = Vector2.zero;
        if (Input.GetKey (KeyCode.UpArrow)) {
            //axisPlayer.MoveUp();
            axisPlayer.inputs.y += 1f;
        }
		if (Input.GetKey (KeyCode.LeftArrow)) {
            //axisPlayer.MoveLeft();
            axisPlayer.inputs.x -= 1f;
        }
		if (Input.GetKey (KeyCode.RightArrow)) {
            //axisPlayer.MoveRight();
            axisPlayer.inputs.x += 1f;
        }
		if (Input.GetKey (KeyCode.DownArrow)) {
            //axisPlayer.MoveDown();
            axisPlayer.inputs.y -= 1f;
        }
		if (Input.GetKey (KeyCode.Period)) {
			axisPlayer.GrabBomb();
		}
		if (Input.GetKey (KeyCode.Slash)) {
			axisPlayer.Jump ();
        }
        if (axisPlayer.inputs != Vector2.zero && !axisPlayer.isStunned)
        {
            axisPlayer.Move();
            axisPlayer.GetComponent<Animator>().Play("Walk");
        }
        else
        {
            axisPlayer.GetComponent<Animator>().Play("Idle");
        }
    }
		
	private void FellOff(GameObject player){

	}

	private void WinGame(GameObject player){

	}

}
