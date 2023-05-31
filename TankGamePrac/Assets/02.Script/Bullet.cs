using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public int power = 1000;  //포 발사 속도
    public AudioClip sound; //사운드 파일 저장
    public GameObject exp;   //폭발효과
    public GameObject bigexp;  //큰폭발효과
    public int hitcount = 0;

    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power); //z축으로 발사
    }
   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) //col=물체에 닿았을 때
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);  //사운드 파일을 transform.position의 위치에서 재생
        GameObject copy_exp = Instantiate(exp) as GameObject;

        if (col.gameObject.tag == "WALL") //닿은 물체의 태그가 WALL이라면
        {
            copy_exp.transform.position = col.transform.position; //효과 위치를 부딪힌 옵젝 위치로
            Destroy(col.gameObject);  //벽을 없앰
        }
        else if (col.gameObject.tag == "Enemy") 
        {
 
            Score.Hit++;
            Debug.Log("enemy");
            if (Score.Hit > 5)
            {
                //enemy제거
                //copy_bigexp.transform.position = col.transform.position;
                //승리화면 Scene전환
                SceneManager.LoadScene("Win");
            }
        }
        else if (col.gameObject.tag == "Target")
        {
            Score.Hit++;
            Debug.Log("아얏");
            if (Score.Hit > 5)
            {
                //target(아군탱크)제거
                //패배 화면 Scene전환
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
        Destroy(gameObject);  //총알을 없앰
        Destroy( copy_exp, 1.5f);
    }
}
