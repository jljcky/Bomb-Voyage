using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public GameObject selectedItem;
    //private float origY;

	// Use this for initialization
	void Start () {
        //origY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, 30f*Time.deltaTime);
        //transform.position = new Vector3(transform.position.x, origY + 0.5f*Mathf.Sin(Time.time), transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Player>().setSelectedBomb(selectedItem);
            Destroy(gameObject);
        }
    }
}
