using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject explosion;
    public string type;

    private float bombCharge;
	private float maxCharge = 2;

	private bool isThrown = false;

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
		if (this.transform.position.y <= 0) {
			GameObject e = Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

    void OnTriggerEnter(Collider other)
    {
        explode();
    }

    private void explode(){
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

    public float getBombCharge(){
        return bombCharge;
    }
}
