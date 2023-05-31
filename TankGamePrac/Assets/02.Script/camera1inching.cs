using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera1inching : MonoBehaviour
{
    public Transform targetTr;
    private Transform camTr;

    [Range(2.0f, 20.0f)] public float distance = 8.0f;
    [Range(1.0f, 10.0f)] public float height = 5.0f;

 
    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        camTr.position = targetTr.position
            + (-targetTr.forward * distance)
            + (Vector3.up * height);

        camTr.LookAt(targetTr.position);
    }
}
