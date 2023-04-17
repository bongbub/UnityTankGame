using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public GameObject player;  //따라다닐 게임 오브젝트 받아옴
    public Vector3 pos;

    void Start()
    {
        pos = this.transform.position;
    }

    void Update()
    {
        transform.position = pos + player.transform.position;
    }
}
