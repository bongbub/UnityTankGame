using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public string[] sentences;  //대화를 넣을 string 배열
    //말풍선 생성 위치를 담을 Transform 변수 선언
    public Transform chatTr;
    //만들었던 ChatBox를 복사하기 위해 선언
    public GameObject ChatBoxPrefab;
    private Transform npcTr;
    public Transform targetTr;

    public void TalkNpc()
    {
        GameObject go = Instantiate(ChatBoxPrefab);
        //복제한 chatBox의 chatSystem 스크립트의 Ondialogue 호출
        go.GetComponent<ChatSystem>().Ondialogue(sentences,chatTr); //인자로 sentences와 chatTr을 넘겨줌
    }

    private void OnMouseDown()
    {
        TalkNpc();
    }

    void Start()
    {
        npcTr = GetComponent<Transform>();
    }
    void Update()
    {
        npcTr.LookAt(targetTr.position);
    }


    /*
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            TalkNpc();
        }
        
    }
    */


}
