using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieCtrl : MonoBehaviour
{

    //몬스터의 상태 정보
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }
    //몬스터의 현재상태
    public State state = State.IDLE; //State에 state라는 변수를 선언하고 그 안에 State enum 안에있는 IDLE을 넣어줌
    //추적 사정거리
    public float traceDist = 10.0f;
    //공격 사정 거리
    public float attackDist = 2.0f;
    //몬스터 사망 여부
    public bool isDie = false;

    //컴포넌트 캐쉬를 처리할 변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator anim;
    private int hitCount = 0;



    //Animator 파라미터의 해쉬값 추출
    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");


    void Start()
    {
        //몬스터의 Transform 할당
        monsterTr = this.gameObject.GetComponent<Transform>(); //this.gameObject 생략해도 됨
        //추적 대상인 Player의 Transform 할당ㅇ
        playerTr = GameObject.FindWithTag("Target").GetComponent<Transform>();
        //NavMeshAgent할당
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //추적 대상의 위치를 설정하면 바로 추적
        //nvAgent.destination = playerTr.position;
        //nvAgent.SetDestination(playerTr.position);

        anim = GetComponent<Animator>();

        //동시에 실행 (상태를 조사, 액션 처리 - 애니메이션, 따라가기)
        //코루틴 함수로 동시에 실행한다는 것이 포인트
        //몬스터의 상태를 체크하는 코루틴 함수를 구동
        StartCoroutine(CheckMonsterState());
        //상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //0.3초 동안 중지(대기) 하는 동안 제어권을 메세지 루프에게 전달
            yield return new WaitForSeconds(0.3f);

            //몬스터와 주인공 캐릭터 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            //공격 사정거리 안으로 들어왔는지 확인
            if (distance <= attackDist)
            {
                state = State.ATTACK;  //상태전환 -> 공격상태
            }

            //추적 사정거리 범위로 들어왔는지 확인
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
                case State.IDLE:  //IDLE상태
                    //추적애니메이션 -> 아이들애니메이션
                    anim.SetBool("IsTrace", false);
                    nvAgent.isStopped = true;  //IDEL상태일 땐 따라가기 멈춤
                    break;
                case State.TRACE:
                    //Animator에 있던 변수명과 같아야함
                    anim.SetBool("IsTrace", true);  //IDLE 애니메이션 -> TRACE 애니메이션
                    anim.SetBool("IsAttack", false); //공격애니메이션 중지
                    //TRACE상태일 땐 따라가기
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false; //플레이어의 위치를 갱신하며 따라가기
                    break;
                case State.ATTACK:
                    anim.SetBool("IsAttack", true);  //공격 애니메이션
                    break;
                case State.DIE:
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnDrawGizmos()
    {
        //추적 사정거리 표시
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);  //와이어프레임그리기(위치, 반지름)
        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    void OnCollisionEnter(Collision coll) //충돌이 됐을 때 호출되는 함수
    {
        if (coll.collider.CompareTag("Bullet")) //총알이랑 부딪혔을 때
        {
            if (coll.gameObject.tag == "Bullet")
            {
                Debug.Log("+1");
                Destroy(coll.gameObject);//총알을 없애주고


            }
            
        }
        Destroy(coll.gameObject);
    }


}