using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public int power = 1000;  //�� �߻� �ӵ�
    public AudioClip sound; //���� ���� ����
    public GameObject exp;   //����ȿ��
    public GameObject bigexp;  //ū����ȿ��
    public int hitcount = 0;

    
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
                //copy_bigexp.transform.position = col.transform.position;
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
        else if (col.gameObject.tag == "Zombie")
        {

            Destroy(col.gameObject);

            
        }
        else if (col.gameObject.tag == "Rock")
        {
            copy_exp.transform.position = this.transform.position;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        copy_exp.transform.position = this.transform.position;
        Destroy(gameObject);  //�Ѿ��� ����
        Destroy( copy_exp, 1.5f);
    }
}
