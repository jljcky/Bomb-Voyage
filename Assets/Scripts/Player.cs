﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int health;
    private float speed;
	private Rigidbody rb;

    public Vector2 inputs;

    public bool isStunned = false;
    public bool isGrounded = false;
    private Quaternion prevRot;

    private float stunned = 1f;
    private float stunnedCD = 0f;

	public GameObject bombPrefab;
	private GameObject heldBomb;

	public float playerMovementModifier = 0.4f;

	// Use this for initialization
	void Start () {
        health = 5;
        speed = 20f;
		rb = gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update () {
        if (isStunned && isGrounded)
        {
            if (stunnedCD >= stunned)
            {
                isStunned = false;
                stunnedCD = 0f;
                Recover();
            }
            else
            {
                stunnedCD += Time.deltaTime;
            }
        }else{
            stunnedCD = 0f;
        }
    }

    //if we want to use a raycast then we dont need these collision events
    void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contacts = collision.contacts;
        foreach (ContactPoint contact in contacts)
        {
            isGrounded |= contact.otherCollider.tag == "Terrain";
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //Grab Bomb, and if already holding then throw
    public void grabBomb(){
		//if (heldBomb == null) {
			heldBomb = Instantiate (bombPrefab, this.transform);
			heldBomb.transform.localPosition = new Vector3(0f, 8.5f, 1f);
		//} 
	}
	public void throwBomb (){
		heldBomb.GetComponent<Collider> ().isTrigger = true;
		heldBomb.AddComponent<Rigidbody> ();
		heldBomb.GetComponent<Rigidbody> ().useGravity = true;
		heldBomb.GetComponent<Rigidbody> ().AddForce (transform.forward*15f+Vector3.up*8f, ForceMode.VelocityChange);
		heldBomb.transform.parent = null;
		heldBomb.GetComponent<Bomb>().setThrown();
	}

	public bool getHeldBomb(){
		if (heldBomb == null)
			return false;
		return true;
	}

	public bool heldBombThrown(){
		if (heldBomb.GetComponent<Bomb> ().getThrown ())
			return true;
		return false;
	}

    public void Move(){
        Vector3 lookAt = new Vector3(inputs.x, 0.0f, inputs.y);
        transform.forward = lookAt;
		rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime * playerMovementModifier);
    }

	public void Jump(){
		rb.AddForce(Vector3.up * 20f * playerMovementModifier, ForceMode.VelocityChange);
	}

	public void GetHit(float bombCharge, Vector3 bombPosition)
    {
        prevRot = transform.localRotation;
        isStunned = true;
        rb.constraints = RigidbodyConstraints.None;
		rb.AddExplosionForce(500f*bombCharge, bombPosition, 100f, 5f);

		if (heldBomb != null && !heldBombThrown()) {
			Destroy (heldBomb);
		}
    }

    public void Recover(){
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.localRotation = prevRot;
        //set position
        //Transform player = transform.Find("Player");
        //Renderer playerMesh = player.GetComponent<Renderer>();
        //transform.position = playerMesh.bounds.center;
    }

	public int loseHealth(){
		health -= 1;
		return health;
	}

    public void setSelectedBomb(GameObject selectedBomb){
        bombPrefab = selectedBomb;
    }

    public GameObject getSelectedBomb(){
        return bombPrefab;
    }
}
