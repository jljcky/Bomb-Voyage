using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	private int health;
    private float speed;
	private Rigidbody rb;

    //public Vector2 inputs;

    public bool isStunned = false;
    public bool isGrounded = false;
    private Quaternion prevRot;

    private float stunned = 1f;
    private float stunnedCD = 0f;

    public GameObject hearts;
    public GameObject heart;

	public GameObject defaultBombPrefab;
    private GameObject specialBomb;
	private GameObject heldBomb;
    public GameObject specialSlot;
    private GameObject specialSlotItem;

	public float playerMovementModifier = 0.4f;

	// Use this for initialization
	void Start () {
        health = 5;
        speed = 20f;
		rb = GetComponent<Rigidbody>();
        for (int i = 0; i < health; i++){
            GameObject h = Instantiate(heart, hearts.transform);
        }
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
	}
	public void throwBomb (){
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
		rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime * playerMovementModifier);
    }

	public void Jump(){
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

    public void Recover(){
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.localRotation = prevRot;
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
        stunnedCD = 0f;
        specialBomb = null;
        Destroy(heldBomb);
        Destroy(specialSlotItem);
        rb.velocity = Vector3.zero;
        Recover();
    }
}
