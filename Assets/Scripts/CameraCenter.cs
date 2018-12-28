using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour {

    public GameObject allyPlayer;
    public GameObject axisPlayer;
    private Vector3 startPos;
    private Vector3 velocity;

    void Start()
    {
        startPos = transform.position;
        velocity = Vector3.zero;
    }

    void Update()
    {
        Vector3 pos1 = allyPlayer.transform.position;
        Vector3 pos2 = axisPlayer.transform.position;
        pos1.y = 0f;
        pos2.z = 0f;
        Vector3 midpoint = pos1 + (pos2 - pos1) / 2f;
        transform.position = Vector3.SmoothDamp(transform.position, startPos+midpoint, ref velocity, 10f);
    }
}
