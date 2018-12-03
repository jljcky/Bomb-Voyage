using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {
    public GameObject[] items;
    //private Vector3 center;
    private float itemDropTime = 20f;
    private float itemCDTime = 0f;
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, 5f*Time.deltaTime);
        if (itemCDTime >= itemDropTime)
        {
            Instantiate(items[Random.Range(0, items.Length)], transform.position, transform.rotation);
            itemCDTime = 0f;
        }
        else
        {
            itemCDTime += Time.deltaTime;
        }
    }
}
