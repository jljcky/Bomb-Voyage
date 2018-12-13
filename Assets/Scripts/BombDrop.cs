using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDrop : MonoBehaviour
{
    [System.Serializable]
    public class ItemDropData
    {
        public GameObject item;
        public int rate;
    }

    public GameObject bomb;
    public MeshRenderer[] dropLocations;
    //private Vector3 center;
    private float bombDropTime = 2f;
    private float bombCDTime = 0f;

    // Update is called once per frame
    void Update()
    {
        MeshRenderer placeToDrop = dropLocations[(int)Random.Range(0f, dropLocations.Length)];
        if (bombCDTime >= bombDropTime && placeToDrop != null)
        {
            Bounds bounds = placeToDrop.bounds;
            float minX = transform.TransformPoint(bounds.center).x - bounds.size.x / 2f;
            float maxX = transform.TransformPoint(bounds.center).x + bounds.size.x / 2f;
            float minZ = transform.TransformPoint(bounds.center).z - bounds.size.z / 2f;
            float maxZ = transform.TransformPoint(bounds.center).z + bounds.size.z / 2f;
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            GameObject b = Instantiate(bomb, new Vector3(randomX, 50f, randomZ), Quaternion.identity);
            b.GetComponent<Bomb>().setThrown();
            b.AddComponent<Rigidbody>();
            b.GetComponent<Collider>().isTrigger = true;
            bombCDTime = 0f;
        }
        else
        {
            bombCDTime += Time.deltaTime;
        }
    }
}
