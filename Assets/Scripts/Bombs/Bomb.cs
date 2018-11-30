using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject explosion;

    protected float bombCharge;
	protected float maxCharge = 2;

	protected bool isThrown = false;

	// Use this for initialization
	void Start () {
        bombCharge = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		//Increase Bomb Charges
        if (bombCharge < maxCharge && !isThrown)
        {
            bombCharge += 0.1f * Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, (bombCharge-1f)/(maxCharge-1f));
        }
        if (this.transform.position.y <= GameSystem.minimumHeight) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	protected virtual void OnTriggerEnter(Collider other)
    {
        explode();
    }

	protected virtual void explode(){
        Collider[] collidersNearby = Physics.OverlapSphere(transform.position, 8f * bombCharge);
        foreach (Collider c in collidersNearby)
        {
            if (c.gameObject.layer == 9)
            {
                c.GetComponent<Player>().GetHit(bombCharge, transform.position);
            }
        }
        GameObject e = Instantiate(explosion, transform.position, transform.rotation);
        e.transform.localScale = Vector3.one * bombCharge;
        Destroy(gameObject);
    }

    public void setThrown(){
		isThrown = true;
	}

	public bool getThrown(){
		return isThrown;
	}

    public void setBombCharge(float bc){
        bombCharge = bc;
    }

    public float getBombCharge(){
        return bombCharge;
    }
}
