using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //�ܺ� ���� ����
    //public GameObject target;  //����ٴ� ��ü �ν��Ͻ�â���� �Ҵ�
    public GameObject bullet;  //�Ѿ� ������Ʈ
    public GameObject spPoint; //�Ѿ� ������ ����Ʈ
    


    //���� ���� ����
    private Transform target;   //Ÿ�� ��ġ ����
    private int rotAngle;       //enemy ȸ������
    private float amtToRot;     //enemy ȸ��
    private int power;          //�Ѿ� �߻� �Ŀ�
    private float distance;     //Ÿ�ٰ� enemy ���� �Ÿ�
    private Vector3 direction;  //enemy�� Ÿ���� �ٶ󺸴� ����
    private float moveSpeed;    //enemy �̵� �ӵ�
    private float fTime;        //����

    private NavMeshAgent nvAgent;



    void Start()
    {
        //�±װ� Target�� ���ӿ�����Ʈ�� ã�Ƽ� �ش� ������Ʈ�� ��ġ ������Ʈ�� target�� ����
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        moveSpeed = 0.45f;
        power = 50;
        fTime = 0.5f;
        rotAngle = 15;

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        //�Ʊ� ��ũ �������� ȸ��
        //transform.LookAt(target.transform.position);

        //�Ѿ��� ��ũ������ ������ ������ �� (Ÿ��������-enemy������)
        direction = target.transform.position - this.transform.position;
        //Vector3.Distance(Vector3 a, Vector3 b) - a�� b ���̿� �Ÿ��� ������ ��ȯ�ϴ� �Լ�
        distance = Vector3.Distance(target.transform.position, this.transform.position);
        fTime += Time.deltaTime;
        //Debug.Log(direction);

        if (distance < 20.0f)
        {
            //�� ��ũ �������
            //this.transform.LookAt(target.transform.position);  //enemy�� target�� �ٶ󺸰�
            //amtToRot = rotAngle * Time.deltaTime;
            //Transform.RotateAround(Vector3 point, Vector3 axis, float angle)
            //point - ������ / axis - �����̴¹���(����) / angle - �����̴� �ӵ�
            //transform.RotateAround(Vector3.zero, Vector3.up, amtToRot);
            //static Vector3.Lerp(Vector3 from, Vector3 to, float t);
            //from�� to�� �ð��� ���� ��ġ�� ���� �� ���
            //this.transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed / 2);

            nvAgent.destination = target.position;


            if (fTime > 1.0f)
            {
                //�Ѿ� ���� �� ���� ó��
                GameObject obj = Instantiate(bullet) as GameObject;
                obj.transform.position = spPoint.transform.position;
                obj.GetComponent<Rigidbody>().AddForce(direction * power);

                fTime = 0.0f;
            }
        }
    }
}
