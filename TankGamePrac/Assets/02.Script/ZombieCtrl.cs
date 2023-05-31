using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCtrl : MonoBehaviour
{

    //������ ���� ����
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    //������ �������
    public State state = State.IDLE; //State�� state��� ������ �����ϰ� �� �ȿ� State enum �ȿ��ִ� IDLE�� �־���
    //���� �����Ÿ�
    public float traceDist = 10.0f;
    //���� ���� �Ÿ�
    public float attackDist = 2.0f;
    //���� ��� ����
    public bool isDie = false;

    //������Ʈ ĳ���� ó���� ����
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator anim;
    private int hitCount = 0;



    //Animator �Ķ������ �ؽ��� ����
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");


    void Start()
    {
        //������ Transform �Ҵ�
        monsterTr = this.gameObject.GetComponent<Transform>(); //this.gameObject �����ص� ��
        //���� ����� Player�� Transform �Ҵ礷
        playerTr = GameObject.FindWithTag("Target").GetComponent<Transform>();
        //NavMeshAgent�Ҵ�
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //���� ����� ��ġ�� �����ϸ� �ٷ� ����
        //nvAgent.destination = playerTr.position;
        //nvAgent.SetDestination(playerTr.position);

        anim = GetComponent<Animator>();

        //���ÿ� ���� (���¸� ����, �׼� ó�� - �ִϸ��̼�, ���󰡱�)
        //�ڷ�ƾ �Լ��� ���ÿ� �����Ѵٴ� ���� ����Ʈ
        //������ ���¸� üũ�ϴ� �ڷ�ƾ �Լ��� ����
        StartCoroutine(CheckMonsterState());
        //���¿� ���� ������ �ൿ�� �����ϴ� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3�� ���� ����(���) �ϴ� ���� ������� �޼��� �������� ����
            yield return new WaitForSeconds(0.3f);

            //���Ϳ� ���ΰ� ĳ���� ������ �Ÿ� ����
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //���� �����Ÿ� ������ ���Դ��� Ȯ��
            if (distance <= attackDist)
            {
                state = State.ATTACK;  //������ȯ -> ���ݻ���
            }

            //���� �����Ÿ� ������ ���Դ��� Ȯ��
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case State.IDLE:  //IDLE����
                    //�����ִϸ��̼� -> ���̵�ִϸ��̼�
                    anim.SetBool("IsTrace", false);
                    nvAgent.isStopped = true;  //IDEL������ �� ���󰡱� ����
                    break;
                case State.TRACE:
                    //Animator�� �ִ� ������� ���ƾ���
                    anim.SetBool("IsTrace", true);  //IDLE �ִϸ��̼� -> TRACE �ִϸ��̼�
                    anim.SetBool("IsAttack", false); //���ݾִϸ��̼� ����
                    //TRACE������ �� ���󰡱�
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false; //�÷��̾��� ��ġ�� �����ϸ� ���󰡱�
                    break;
                case State.ATTACK:
                    anim.SetBool("IsAttack", true);  //���� �ִϸ��̼�
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnDrawGizmos()
    {
        //���� �����Ÿ� ǥ��
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);  //���̾������ӱ׸���(��ġ, ������)
        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    void OnCollisionEnter(Collision coll) //�浹�� ���� �� ȣ��Ǵ� �Լ�
    {
        if (coll.collider.CompareTag("Bullet")) //�Ѿ��̶� �ε����� ��
        {
            if (coll.gameObject.tag == "Bullet")
            {
                Debug.Log("+1");
                Destroy(coll.gameObject);//�Ѿ��� �����ְ�


            }
            
        }
        Destroy(coll.gameObject);
    }


}