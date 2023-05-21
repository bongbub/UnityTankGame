using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public int power = 1000;  //�� �߻� �ӵ�
    public AudioClip sound; //���� ���� ����
    public GameObject exp;   //����ȿ��

    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power); //z������ �߻�
    }
   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) //col=��ü�� ����� ��
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);  //���� ������ transform.position�� ��ġ���� ���
        GameObject copy_exp = Instantiate(exp) as GameObject;

        if (col.gameObject.tag == "WALL") //���� ��ü�� �±װ� WALL�̶��
        {
            copy_exp.transform.position = col.transform.position; //ȿ�� ��ġ�� �ε��� ���� ��ġ��
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
                //SceneManager.LoadScene("Lose");
            }
        }
        copy_exp.transform.position = this.transform.position;
        Destroy(gameObject);  //�Ѿ��� ����
    }
}
