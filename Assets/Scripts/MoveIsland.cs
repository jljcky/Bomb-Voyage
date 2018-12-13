using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsland : MonoBehaviour {
    public Vector3 position1;
    public Vector3 position2;
    public float offset = 0f;
    private float moveTime = 20f;
    //private Vector3[] positions =
    //{
    //    new Vector3(42.5f, 0f, 42.5f),
    //    new Vector3(-42.5f, 0f, 42.5f),
    //    new Vector3(-42.5f, 0f, -42.5f),
    //    new Vector3(42.5f, 0f, -42.5f)
    //};
    void Start()
    {
        StartCoroutine(OscillateIsland());
    }

    private IEnumerator OscillateIsland()
    {
        float t = offset;
        while (true)
        {
            while (t < moveTime)
            {
                transform.position = Vector3.Lerp(position1, position2, t / moveTime);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            t = 0f;
            while (t < moveTime)
            {
                transform.position = Vector3.Lerp(position2, position1, t / moveTime);
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            t = 0f;
        }
    }
}
