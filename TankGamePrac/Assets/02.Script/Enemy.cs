using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //외부 세팅 변수
    //public GameObject target;  //따라다닐 객체 인스턴스창에서 할당
    public GameObject bullet;  //총알 오브젝트
    public GameObject spPoint; //총알 나가는 포인트
    


    //내부 세팅 변수
    private Transform target;   //타겟 위치 저장
    private int rotAngle;       //enemy 회전각도
    private float amtToRot;     //enemy 회전
    private int power;          //총알 발사 파워
    private float distance;     //타겟과 enemy 사이 거리
    private Vector3 direction;  //enemy가 타겟을 바라보는 방향
    private float moveSpeed;    //enemy 이동 속도
    private float fTime;        //공속

    private NavMeshAgent nvAgent;



    void Start()
    {
        //태그가 Target인 게임오브젝트를 찾아서 해당 오브젝트의 위치 컴포넌트를 target에 저장
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        moveSpeed = 0.45f;
        power = 50;
        fTime = 0.5f;
        rotAngle = 15;

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        //아군 탱크 방향으로 회전
        //transform.LookAt(target.transform.position);

        //총알이 탱크쪽으로 나가는 방향을 얻어냄 (타겟포지션-enemy포지션)
        direction = target.transform.position - this.transform.position;
        //Vector3.Distance(Vector3 a, Vector3 b) - a와 b 사이에 거리를 측정해 반환하는 함수
        distance = Vector3.Distance(target.transform.position, this.transform.position);
        fTime += Time.deltaTime;
        //Debug.Log(direction);

        if (distance < 20.0f)
        {
            //적 탱크 따라오게
            //this.transform.LookAt(target.transform.position);  //enemy가 target을 바라보게
            //amtToRot = rotAngle * Time.deltaTime;
            //Transform.RotateAround(Vector3 point, Vector3 axis, float angle)
            //point - 기준점 / axis - 움직이는방향(대충) / angle - 움직이는 속도
            //transform.RotateAround(Vector3.zero, Vector3.up, amtToRot);
            //static Vector3.Lerp(Vector3 from, Vector3 to, float t);
            //from과 to의 시간에 따른 위치를 구할 때 사용
            //this.transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed / 2);

            nvAgent.destination = target.position;


            if (fTime > 1.0f)
            {
                //총알 생성 및 사운드 처리
                GameObject obj = Instantiate(bullet) as GameObject;
                obj.transform.position = spPoint.transform.position;
                obj.GetComponent<Rigidbody>().AddForce(direction * power);

                fTime = 0.0f;
            }
        }
    }
}
