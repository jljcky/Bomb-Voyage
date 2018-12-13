using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [System.Serializable]
    public class ItemDropData
    {
        public GameObject item;
        public int rate;
    }

    public ItemDropData[] items;
    public MeshRenderer[] dropLocations;
    //private Vector3 center;
    private float itemDropTime = 0.5f;
    private float itemCDTime = 0f;
	
	// Update is called once per frame
	void Update () {
        //transform.RotateAround(Vector3.zero, Vector3.up, 5f*Time.deltaTime);
        MeshRenderer placeToDrop = dropLocations[(int)Random.Range(0f, dropLocations.Length)];
        if (itemCDTime >= itemDropTime && placeToDrop != null)
        {
            Bounds bounds = placeToDrop.bounds;
            float minX = transform.TransformPoint(bounds.center).x - bounds.size.x / 2f;
            float maxX = transform.TransformPoint(bounds.center).x + bounds.size.x / 2f;
            float minZ = transform.TransformPoint(bounds.center).z - bounds.size.z / 2f;
            float maxZ = transform.TransformPoint(bounds.center).z + bounds.size.z / 2f;
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            float sum = 0f;
            foreach (ItemDropData d in items)
            {
                sum += d.rate;
            }
            int rate = (int)Random.Range(0,sum+1);
            GameObject item = items[0].item;
            int check = 0;
            foreach (ItemDropData d in items)
            {
                check += d.rate;
                if (rate <= check)
                {
                    item = d.item;
                    break;
                }
            }
            Instantiate(item, new Vector3(randomX, 50f, randomZ), Quaternion.identity);
            itemCDTime = 0f;
        }
        else
        {
            itemCDTime += Time.deltaTime;
        }
    }
}
