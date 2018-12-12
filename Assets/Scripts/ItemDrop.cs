using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [System.Serializable]
    public class ItemDropData
    {
        public GameObject item;
        public float rate;
    }

    public ItemDropData[] items;
    //private Vector3 center;
    private float itemDropTime = 20f;
    private float itemCDTime = 0f;
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, 5f*Time.deltaTime);
        if (itemCDTime >= itemDropTime)
        {
            float sum = 0f;
            foreach (ItemDropData d in items)
            {
                sum += d.rate;
            }
            float rate = Random.Range(0,sum+1);
            GameObject item = null;
            float check = 0f;
            foreach (ItemDropData d in items)
            {
                check += d.rate;
                if (rate <= check)
                {
                    item = d.item;
                    break;
                }
            }
            Instantiate(item, transform.position, transform.rotation);
            itemCDTime = 0f;
        }
        else
        {
            itemCDTime += Time.deltaTime;
        }
    }
}
