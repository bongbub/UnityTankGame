using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public string[] sentences;  //��ȭ�� ���� string �迭
    //��ǳ�� ���� ��ġ�� ���� Transform ���� ����
    public Transform chatTr;
    //������� ChatBox�� �����ϱ� ���� ����
    public GameObject ChatBoxPrefab;
    private Transform npcTr;
    public Transform targetTr;

    public void TalkNpc()
    {
        GameObject go = Instantiate(ChatBoxPrefab);
        //������ chatBox�� chatSystem ��ũ��Ʈ�� Ondialogue ȣ��
        go.GetComponent<ChatSystem>().Ondialogue(sentences,chatTr); //���ڷ� sentences�� chatTr�� �Ѱ���
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
