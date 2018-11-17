using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour {

	public GameObject allyPlayerPrefab;
	public GameObject axisPlayerPrefab;
	private GameObject allyPlayerObject;
	private GameObject axisPlayerObject;
	private Player allyPlayer;
	private Player axisPlayer;
    private Animator allyAnimator;
    private Animator axisAnimator;
	private int minimumHeight = 0;
	public Vector3 allyPlayerStartPosition;
	public Vector3 axisPlayerStartPosition;

	public float timeScaleModifier = 4.0f;

	// Use this for initialization
	void Start () {
		allyPlayerObject = Instantiate (allyPlayerPrefab);
		axisPlayerObject = Instantiate (axisPlayerPrefab);
        allyPlayerObject.transform.position = allyPlayerStartPosition;
        axisPlayerObject.transform.position = axisPlayerStartPosition;
        allyPlayer = allyPlayerObject.GetComponent<Player> ();
		axisPlayer = axisPlayerObject.GetComponent<Player> ();
        allyAnimator = allyPlayerObject.GetComponent<Animator>();
        axisAnimator = axisPlayerObject.GetComponent<Animator>();
		Time.timeScale = timeScaleModifier;
    }
	
	// Update is called once per frame
	void Update () {
		//Check if any Player fell off
		if (allyPlayerObject.transform.position.y < minimumHeight){
			FellOff (allyPlayer);
		}
		if (axisPlayerObject.transform.position.y < minimumHeight){
			FellOff (axisPlayer);
		}

        //Ally Player Movement
        if (!allyPlayer.isStunned)
        {
            allyPlayer.inputs = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                allyPlayer.inputs.y += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                allyPlayer.inputs.x -= 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                allyPlayer.inputs.y -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                allyPlayer.inputs.x += 1f;
            }
			if (Input.GetKey(KeyCode.E) && !allyPlayer.getHeldBomb())
            {
                allyAnimator.SetBool("isCharging", true);
				allyPlayer.grabBomb();
            }
			if (Input.GetKeyUp(KeyCode.E) && allyPlayer.getHeldBomb() && !allyPlayer.heldBombThrown())
            {
                allyAnimator.SetBool("isCharging", false);
				allyPlayer.throwBomb();
            }
            if (allyPlayer.inputs != Vector2.zero)
            {
                allyPlayer.Move();
                allyAnimator.SetBool("isWalking", true);
            }
            else
            {
                allyAnimator.SetBool("isWalking", false);
            }
            if (Input.GetKeyDown(KeyCode.R) && allyPlayer.isGrounded)
            {
                allyPlayer.Jump();
                allyAnimator.SetTrigger("jump");
            }
            allyAnimator.SetBool("isGrounded", allyPlayer.isGrounded);

        }
        else
        {
            allyAnimator.Play("Idle");
			allyAnimator.SetBool("isCharging", false);
        }

        //Axis Player Movement
        if (!axisPlayer.isStunned)
        {
            axisPlayer.inputs = Vector2.zero;
            if (Input.GetKey (KeyCode.UpArrow)) {
                axisPlayer.inputs.y += 1f;
            }
    		if (Input.GetKey (KeyCode.LeftArrow)) {
                axisPlayer.inputs.x -= 1f;
            }
    		if (Input.GetKey (KeyCode.RightArrow)) {
                axisPlayer.inputs.x += 1f;
            }
    		if (Input.GetKey (KeyCode.DownArrow)) {
                axisPlayer.inputs.y -= 1f;
            }
			if (Input.GetKey(KeyCode.Period) && !axisPlayer.getHeldBomb())
			{
				axisAnimator.SetBool("isCharging", true);
				axisPlayer.grabBomb();
			}
			if (Input.GetKeyUp(KeyCode.Period) && axisPlayer.getHeldBomb() && !axisPlayer.heldBombThrown())
			{
				axisAnimator.SetBool("isCharging", false);
				axisPlayer.throwBomb();
			}
			if (axisPlayer.inputs != Vector2.zero)
			{
				axisPlayer.Move();
				axisAnimator.SetBool("isWalking", true);
			}
			else
			{
				axisAnimator.SetBool("isWalking", false);
			}
			if (Input.GetKeyDown(KeyCode.Slash) && axisPlayer.isGrounded)
			{
				axisPlayer.Jump();
				axisAnimator.SetTrigger("jump");
			}
			axisAnimator.SetBool("isGrounded", axisPlayer.isGrounded);
        }
        else
        {
			axisAnimator.SetBool("isCharging", false);
            axisAnimator.Play("Idle");
        }
    }
		
	private void FellOff(Player player){
		player.loseHealth();
		SceneManager.LoadScene("StartScreen");
	}

	private void WinGame(GameObject player){

	}

}
