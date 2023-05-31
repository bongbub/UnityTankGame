using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoru : MonoBehaviour
{
    Coroutine rotCoroutine = null;
    public float rotSpeed = 30.0f;

    public void RotStartFunc()
    {
        rotCoroutine = StartCoroutine(CoroutineRotFunc());
    }

    IEnumerator CoroutineRotFunc()
    {
        var angles = transform.rotation.eulerAngles;
        angles.y -= Time.deltaTime * rotSpeed;
        transform.rotation = Quaternion.Euler(angles);

        yield return null;

        rotCoroutine = StartCoroutine(CoroutineRotFunc());
    }

    public void RotStopFunc()
    {
        if (rotCoroutine != null)
            StopCoroutine(rotCoroutine);
    }

    void Start()
    {
        
    }


    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
