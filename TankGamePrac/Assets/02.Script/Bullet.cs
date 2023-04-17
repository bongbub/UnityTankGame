using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int power = 1500;  //�� �߻� �ӵ�

    public AudioClip sound; //���� ���� ����
    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) //col=��ü�� ����� ��
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);  //���� ������ transform.position�� ��ġ���� ���
        if (col.gameObject.tag == "WALL") //���� ��ü�� �±װ� WALL�̶��
        {
            Destroy(col.gameObject);  //���� ����
        }
        
        Destroy(gameObject);  //�Ѿ��� ����
    }
}
