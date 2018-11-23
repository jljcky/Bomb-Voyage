using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public GameObject bombPrefab;
    private float lifetime = 10f;
    public float itemTimeModifier = 0.25f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 30f*Time.deltaTime);
        if (lifetime <= 0f) {
            Destroy(gameObject);
        }else {
            lifetime -= Time.deltaTime * itemTimeModifier;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Player>().setSelectedBomb(this.gameObject, bombPrefab);
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 10)
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
