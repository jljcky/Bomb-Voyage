﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private int health;
    private float speed;
	private Rigidbody rb;

	public AudioSource jumpClip;
	public AudioSource throwClip;

    //public Vector2 inputs;

    public bool isStunned = false;
    public bool isGrounded = false;
    private Quaternion prevRot;

    private float stunned =0f;
    private float stunnedCD = -2f;
	private float slowedDuration = 0f;
    private float burnDuration = 0f;

    public GameObject hearts;
    public GameObject heart;

	public GameObject defaultBombPrefab;
    private GameObject specialBomb;
	private GameObject heldBomb;
    public GameObject specialSlot;
    private GameObject specialSlotItem;

    private Material prevMaterial;
    //private Material currentMaterial;

	public float playerMovementModifier = 0.4f;

	// Use this for initialization
	void Start () {
        health = 5;
        speed = 20f;
		rb = GetComponent<Rigidbody>();
        for (int i = 0; i < health; i++){
            Instantiate(heart, hearts.transform);
        }
        prevMaterial = transform.Find("Player").GetComponent<Renderer>().material;
    }


    // Update is called once per frame
    void Update () {
        if (isStunned && isGrounded)
        {
            if (stunnedCD >= stunned)
            {
                isStunned = false;
                stunnedCD = -2f;
                Recover();
            }
            else
            {
                stunnedCD += Time.deltaTime;
            }
        }else{
            stunnedCD = -2f;
        }
    }

    //if we want to use a raycast then we dont need these collision events
    void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contacts = collision.contacts;
        foreach (ContactPoint contact in contacts)
        {
            isGrounded |= contact.otherCollider.gameObject.layer == 10;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //Grab Bomb, and if already holding then throw
    public void grabBomb(){
        if (specialBomb == null)
        {
            heldBomb = Instantiate(defaultBombPrefab, this.transform);
        }
        else {
            heldBomb = Instantiate(specialBomb, this.transform);
            specialBomb = null;
            Destroy(specialSlotItem);
        }
		heldBomb.transform.localPosition = new Vector3(0f, 8.5f, 1f);

        if (burnDuration > 0f)
        {
            heldBomb.GetComponent<Bomb>().burnExplode();
        }
    }
	public void throwBomb (){
		throwClip.Play ();
		heldBomb.GetComponent<Collider> ().isTrigger = true;
		heldBomb.AddComponent<Rigidbody> ();
		heldBomb.GetComponent<Rigidbody> ().useGravity = true;
        float bombCharge = heldBomb.GetComponent<Bomb>().getBombCharge();
        heldBomb.GetComponent<Rigidbody> ().AddForce (transform.forward*(5f+20f*(bombCharge-1f))+Vector3.up*8f, ForceMode.VelocityChange);
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
		
    public void Move(float x, float y){
        //Vector3 lookAt = new Vector3(inputs.x, 0.0f, inputs.y);
        Vector3 lookAt = new Vector3(x, 0.0f, y);
        transform.forward = lookAt;
		if (slowedDuration > 0f) {
			rb.MovePosition (transform.position + transform.forward * speed / 4f * Time.deltaTime * playerMovementModifier);
		}
		else {
			rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime * playerMovementModifier);
		}
    }

	public void Jump(){
		jumpClip.Play ();
		rb.AddForce(Vector3.up * 40f * playerMovementModifier, ForceMode.VelocityChange);
	}

	public void GetHit(float bombCharge, Vector3 bombPosition)
    {
        if (!isStunned)
            prevRot = transform.localRotation;
        isStunned = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(500f*bombCharge, bombPosition, 100f*bombCharge, 5f);

		if (heldBomb != null && !heldBombThrown()) {
			Destroy (heldBomb);
		}
    }

	public void setStunnedcd(float cd, Material freezeMat){
		stunnedCD = cd;
        transform.Find("Player").GetComponent<Renderer>().material = freezeMat;

    }

	public void makeSlow(float duration, Material slowMat){
        StopCoroutine("slowTime");
		slowedDuration = duration;
        StartCoroutine(slowTime(slowMat));
    }

    private IEnumerator slowTime(Material slowMat)
    {
        transform.Find("Player").GetComponent<Renderer>().material = slowMat;
        while (slowedDuration > 0f)
        {
            slowedDuration -= Time.deltaTime;
            yield return null;
        }
        transform.Find("Player").GetComponent<Renderer>().material = prevMaterial;
    }


    public void makeBurned(float duration, Material fireMat)
    {
        StopCoroutine("burnTime");
        burnDuration = duration;
        StartCoroutine(burnTime(fireMat));
    }

    private IEnumerator burnTime(Material fireMat)
    {
        transform.Find("Player").GetComponent<Renderer>().material = fireMat;
        while (burnDuration > 0f)
        {
            burnDuration -= Time.deltaTime;
            yield return null;
        }
        transform.Find("Player").GetComponent<Renderer>().material = prevMaterial;
    }

    public void Recover(){
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.localRotation = prevRot;
        if (!prevMaterial.Equals(transform.Find("Player").GetComponent<Renderer>().material) && slowedDuration <= 0f)
        {
            transform.Find("Player").GetComponent<Renderer>().material = prevMaterial;
        }
    }

	public int loseHealth(){
		health -= 1;
        hearts.transform.GetChild(health).gameObject.SetActive(false);
		return health;
	}

    public void setSelectedBomb(GameObject item, GameObject selectedBomb){
        specialBomb = selectedBomb;
        Destroy(specialSlotItem);
        specialSlotItem = Instantiate(item, specialSlot.transform);
        specialSlotItem.transform.localPosition = Vector3.zero;
        Destroy(specialSlotItem.GetComponent<Rigidbody>());
        Destroy(specialSlotItem.GetComponent<Item>());
    }

    public GameObject getSelectedBomb(){
        return specialBomb;
    }

    public int getHealth(){
        return health;
    }

    public void refresh()
    {
        isStunned = false;
        isGrounded = false;
        stunnedCD = -2f;
        slowedDuration = 0f;
        burnDuration = 0f;
        specialBomb = null;
        if (heldBomb != null && !heldBomb.GetComponent<Bomb>().getThrown())
            Destroy(heldBomb);
        Destroy(specialSlotItem);
        rb.velocity = Vector3.zero;
        Recover();
    }
}
