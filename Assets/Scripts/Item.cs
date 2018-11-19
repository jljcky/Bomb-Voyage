using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public GameObject bombPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 30f*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Player>().setSelectedBomb(bombPrefab);
            Destroy(gameObject);
        }
        else if (other.tag == "terrain")
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
