using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int health;
	private float bombCharge;
	private float maxCharge = 5;
    private float speed;
	private Rigidbody rb;

    public Vector2 inputs;

    public bool isStunned = false;
    private Quaternion prevRot;

    private float stunnedGrounded = 1f;
    private float stunnedGroundedCD = 0f;

	// Use this for initialization
	void Start () {
        health = 5;
        speed = 20f;
		rb = gameObject.GetComponent<Rigidbody>();
    }


// Update is called once per frame
void Update () {
		//Increase Bomb Charges
		if (bombCharge > 0 && bombCharge < maxCharge)
			bombCharge += 1f * Time.deltaTime;
	}

    void OnCollisionStay(Collision collision)
    {
        if (isStunned)
        {
            ContactPoint[] contacts = collision.contacts;
            bool isGrounded = false;
            foreach (ContactPoint contact in contacts)
            {
                if (contact.otherCollider.tag == "Terrain")
                {
                    if (stunnedGroundedCD >= stunnedGrounded)
                    {
                        isStunned = false;
                        stunnedGroundedCD = 0f;
                        Recover();
                    }
                    else
                    {
                        stunnedGroundedCD += Time.deltaTime;
                    }
                    isGrounded = true;
                    break;
                }
            }
            if (!isGrounded){
                stunnedGroundedCD = 0f;
            }
        }
    }

    //Grab Bomb, and if already holding then throw
    public bool GrabBomb(){
		if (bombCharge != 0) {
			ThrowBomb ();
			return false;
		} else {
			bombCharge = 1;
			return true;
		}
	}
	private void ThrowBomb (){

	}

	//Movement
	//public void MoveUp(){
 //       rb.MovePosition(transform.forward * speed * Time.deltaTime);
	//}

	//public void MoveDown()
 //   {
 //       rb.MovePosition(-transform.forward * speed * Time.deltaTime);
 //   }

	//public void MoveLeft()
 //   {
 //       rb.MovePosition(-transform.right * speed * Time.deltaTime);
 //   }

	//public void MoveRight()
    //{
    //    rb.MovePosition(transform.right * speed * Time.deltaTime);
    //}

    public void Move(){
        Vector3 lookAt = new Vector3(inputs.x, 0.0f, inputs.y);
        transform.forward = lookAt;
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

	public void Jump(){

	}

    public void GetHit()
    {
        prevRot = transform.localRotation;
        isStunned = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void Recover(){
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.localRotation = prevRot;
        //rb.velocity = Vector3.zero;
    }
}
