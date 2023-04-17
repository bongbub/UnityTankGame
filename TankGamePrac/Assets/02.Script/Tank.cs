using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    #region 탱크이동
    //탱크 앞뒤 이동
    public int moveSpeed;
    public float move;
    public float moveVertical;

    //탱크 좌우이동
    public int rotSpeed;
    public float rotate;
    public float rotHorizon;
    #endregion

    #region 머리 및 포 회전
    //머리 회전
    public float DGRrotate;
    public GameObject DGR;

    //포 회전
    public GameObject Pogijun;
    #endregion

    #region 총알발사 및 삭제
    public int power;  //포 발사 속도
    public GameObject bulletPrefab; //총알
    public Transform spPoint;   //생성위치 받아오기

    //일정시간 후 총알 삭제
    public float DestroyTime = 2.0f;
    #endregion

    void Start()
    {
        moveSpeed = 7;
        rotSpeed = 120;
        power = 1500;
        spPoint = GameObject.Find("spPoint").transform;  //게임오브젝트 중 이름이spPoint인 것을 찾아 위치를 받아옴
    }

    
    void Update()
    {
        
        move = moveSpeed * Time.deltaTime;
        rotate = rotSpeed * Time.deltaTime;

        //상하좌우이동 키 받기
        moveVertical = Input.GetAxis("Vertical");
        rotHorizon = Input.GetAxis("Horizontal");

        this.transform.Translate(Vector3.forward * move * moveVertical);
        this.transform.Rotate(new Vector3(0.0f, rotate * rotHorizon, 0.0f));

        //머리 회전 q,e키 받기
        DGRrotate = Input.GetAxis("Window Shake X"); 
        DGR.transform.Rotate(Vector3.up * DGRrotate * rotate);


        //포 마우스휠 조절
        float keyGun = Input.GetAxis("Mouse ScrollWheel");
        Pogijun.transform.Rotate(Vector3.right * keyGun * 15);

        //포 회전 제한
        Vector3 angle = Pogijun.transform.eulerAngles;
        if (angle.x > 180)
            angle.x -= 360;
        angle.x = Mathf.Clamp(angle.x, -15, 5);
        Pogijun.transform.eulerAngles = angle;

        #region 총알발사 및 삭제
        if (Input.GetMouseButtonDown(0)) //왼쪽 마우스 버튼이 눌렸다면
        {
            GameObject bullet = Instantiate(bulletPrefab, spPoint.position, spPoint.rotation) as GameObject;
            Rigidbody bulletAddforce = bullet.GetComponent<Rigidbody>();
            bulletAddforce.AddForce(DGR.transform.forward * power);
            //총알 삭제
            Destroy(bullet, DestroyTime);
        }

        #endregion
    }
}
