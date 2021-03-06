﻿using System.Collections;
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
	public static int minimumHeight = -50;
	public Vector3 allyPlayerStartPosition;
	public Vector3 axisPlayerStartPosition;
    public GameObject AllyWin;
    public GameObject AxisWin;
    public GameObject Draw;

    private bool isAllyWin = false;
    private bool isAxisWin = false;

	public float timeScaleModifier = 4.0f;

	// Use this for initialization
	void Start () {
        allyPlayerObject = allyPlayerPrefab;
        axisPlayerObject = axisPlayerPrefab;
        allyPlayerObject.transform.position = allyPlayerStartPosition;
        axisPlayerObject.transform.position = axisPlayerStartPosition;
        allyPlayer = allyPlayerObject.GetComponent<Player> ();
		axisPlayer = axisPlayerObject.GetComponent<Player> ();
        allyAnimator = allyPlayerObject.GetComponent<Animator>();
        axisAnimator = axisPlayerObject.GetComponent<Animator>();
		Time.timeScale = timeScaleModifier;
        AllyWin.SetActive(false);
        AxisWin.SetActive(false);
        Draw.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if any Player fell off
        if (allyPlayerObject.transform.position.y < minimumHeight)
        {
            FellOff(allyPlayer);
            if (allyPlayer.getHealth() <= 0)
            {
                allyPlayerObject.transform.position = new Vector3(0f, 999f, 0f);
                isAxisWin = true;
            }
            else
            {
                allyPlayerObject.transform.position = allyPlayerStartPosition;
                allyAnimator.SetBool("isCharging", false);
                allyPlayer.refresh();
            }
        }

        if (axisPlayerObject.transform.position.y < minimumHeight)
        {
            FellOff(axisPlayer);
            if (axisPlayer.getHealth() <= 0)
            {
                axisPlayerObject.transform.position = new Vector3(0f, 999f, 0f);
                isAllyWin = true;
            }
            else
            {
                axisPlayerObject.transform.position = axisPlayerStartPosition;
                axisAnimator.SetBool("isCharging", false);
                axisPlayer.refresh();
            }
        }

        if (isAllyWin && isAxisWin)
        {
            StartCoroutine(DelayReturnMenu(3f, Draw));
            enabled = false;
        }
        else if (isAllyWin)
        {
            StartCoroutine(DelayReturnMenu(3f, AllyWin));
            enabled = false;
        }
        else if (isAxisWin)
        {
            StartCoroutine(DelayReturnMenu(3f, AxisWin));
            enabled = false;
        }

        //Ally Player Movement
        if (!allyPlayer.isStunned)
        {
            //allyPlayer.inputs = Vector2.zero;
            Vector2 inputs = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                inputs.y += 1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputs.x -= 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputs.y -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputs.x += 1f;
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
            if (inputs != Vector2.zero)
            {
                allyPlayer.Move(inputs.x, inputs.y);
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
            Vector2 inputs = Vector2.zero;
            if (Input.GetKey (KeyCode.UpArrow)) {
                  inputs.y += 1f;
            }
            if (Input.GetKey (KeyCode.LeftArrow)) {
                inputs.x -= 1f;
            }
            if (Input.GetKey (KeyCode.RightArrow)) {
                inputs.x += 1f;
            }
            if (Input.GetKey (KeyCode.DownArrow)) {
                inputs.y -= 1f;
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
			if (inputs != Vector2.zero)
			{
				axisPlayer.Move(inputs.x, inputs.y);
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
	}

	//private void WinGame(GameObject WinPanel){
    //    Time.timeScale = 0f;
    //    WinPanel.SetActive(true);
    //}

    private IEnumerator DelayReturnMenu(float seconds, GameObject WinPanel)
    {
        Vector3 localScale = WinPanel.transform.localScale;
        Time.timeScale = 0f;
        WinPanel.SetActive(true);
        WinPanel.transform.localScale = Vector3.zero;
        print(WinPanel.transform.localScale);
        float t = 0f;
        while (t < 0.1f)
        {
            WinPanel.transform.localScale = Vector3.Lerp(Vector3.zero, localScale * 1.2f, t / 0.1f);
            t += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        t = 0f;
        Vector3 currentScale = WinPanel.transform.localScale;
        while (t < 0.1)
        {
            WinPanel.transform.localScale = Vector3.Lerp(currentScale, localScale * 0.6f, t / 0.1f);
            t += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        t = 0f;
        currentScale = WinPanel.transform.localScale;
        while (t < 0.1)
        {
            WinPanel.transform.localScale = Vector3.Lerp(currentScale, localScale, t / 0.1f);
            t += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene("StartScreen");
    }
}
