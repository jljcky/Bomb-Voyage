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
    public MeshRenderer[] dropLocations;
    //private Vector3 center;
    private float itemDropTime = 20f;
    private float itemCDTime = 0f;
	
	// Update is called once per frame
	void Update () {
        //transform.RotateAround(Vector3.zero, Vector3.up, 5f*Time.deltaTime);
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
            MeshRenderer placeToDrop = dropLocations[(int)Random.Range(0f, dropLocations.Length)];
            Bounds bounds = placeToDrop.bounds;
            float minX = bounds.center.x - bounds.size.x / 2f;
            float maxX = bounds.center.x + bounds.size.x / 2f;
            float minZ = bounds.center.z - bounds.size.z / 2f;
            float maxZ = bounds.center.z + bounds.size.z / 2f;
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Instantiate(item, new Vector3(randomX, 50f, randomZ), Quaternion.identity);
            itemCDTime = 0f;
        }
        else
        {
            itemCDTime += Time.deltaTime;
        }
    }
}
