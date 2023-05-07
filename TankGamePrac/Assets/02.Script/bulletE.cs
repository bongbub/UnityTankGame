using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletE : MonoBehaviour
{
    public int power = 1000;  //�� �߻� �ӵ�
    public AudioClip sound; //���� ���� ����

    private Transform target;
    private Transform enemy;
    private Vector3 direction;


    void Start()
    {
        //GetComponent<Rigidbody>().AddForce(transform.forward * power); //z������ �߻�
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        //Debug.Log(direction);
        GetComponent<Rigidbody>().AddForce(direction * power);
    }

    void Update()
    {
        direction = target.transform.position - enemy.transform.position;
        
    }

    void OnTriggerEnter(Collider col) //col=��ü�� ����� ��
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);  //���� ������ transform.position�� ��ġ���� ���
        if (col.gameObject.tag == "WALL") //���� ��ü�� �±װ� WALL�̶��
        {
            Destroy(col.gameObject);  //���� ����
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Score.Hit++;
            Debug.Log("enemy");
            if (Score.Hit > 5)
            {
                //enemy����
                //�¸�ȭ�� Scene��ȯ
                SceneManager.LoadScene("Win");
            }
        }
        else if (col.gameObject.tag == "Target")
        {
            Score.Hit++;
            Debug.Log("�ƾ�");
            if (Score.Hit > 5)
            {
                //target(�Ʊ���ũ)����
                //�й� ȭ�� Scene��ȯ
                SceneManager.LoadScene("Lose");
            }
        }

        Destroy(gameObject);  //�Ѿ��� ����
    }
}
