using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    #region ��ũ�̵�
    //��ũ �յ� �̵�
    public int moveSpeed;
    public float move;
    public float moveVertical;

    //��ũ �¿��̵�
    public int rotSpeed;
    public float rotate;
    public float rotHorizon;
    #endregion

    #region �Ӹ� �� �� ȸ��
    //�Ӹ� ȸ��
    public float DGRrotate;
    public GameObject DGR;

    //�� ȸ��
    public GameObject Pogijun;
    #endregion

    #region �Ѿ˹߻� �� ����
    public int power;  //�� �߻� �ӵ�
    public GameObject bulletPrefab; //�Ѿ�
    public Transform spPoint;   //������ġ �޾ƿ���

    //�����ð� �� �Ѿ� ����
    public float DestroyTime = 2.0f;
    #endregion

    void Start()
    {
        moveSpeed = 7;
        rotSpeed = 120;
        power = 1500;
        spPoint = GameObject.Find("spPoint").transform;  //���ӿ�����Ʈ �� �̸���spPoint�� ���� ã�� ��ġ�� �޾ƿ�
    }

    
    void Update()
    {
        
        move = moveSpeed * Time.deltaTime;
        rotate = rotSpeed * Time.deltaTime;

        //�����¿��̵� Ű �ޱ�
        moveVertical = Input.GetAxis("Vertical");
        rotHorizon = Input.GetAxis("Horizontal");

        this.transform.Translate(Vector3.forward * move * moveVertical);
        this.transform.Rotate(new Vector3(0.0f, rotate * rotHorizon, 0.0f));

        //�Ӹ� ȸ�� q,eŰ �ޱ�
        DGRrotate = Input.GetAxis("Window Shake X"); 
        DGR.transform.Rotate(Vector3.up * DGRrotate * rotate);


        //�� ���콺�� ����
        float keyGun = Input.GetAxis("Mouse ScrollWheel");
        Pogijun.transform.Rotate(Vector3.right * keyGun * 15);

        //�� ȸ�� ����
        Vector3 angle = Pogijun.transform.eulerAngles;
        if (angle.x > 180)
            angle.x -= 360;
        angle.x = Mathf.Clamp(angle.x, -15, 5);
        Pogijun.transform.eulerAngles = angle;

        #region �Ѿ˹߻� �� ����
        if (Input.GetMouseButtonDown(0)) //���� ���콺 ��ư�� ���ȴٸ�
        {
            GameObject bullet = Instantiate(bulletPrefab, spPoint.position, spPoint.rotation) as GameObject;
            Rigidbody bulletAddforce = bullet.GetComponent<Rigidbody>();
            bulletAddforce.AddForce(DGR.transform.forward * power);
            //�Ѿ� ����
            Destroy(bullet, DestroyTime);
        }

        #endregion
    }
}
