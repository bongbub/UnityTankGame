using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public int power = 1000;  //포 발사 속도
    public AudioClip sound; //사운드 파일 저장

    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col) //col=물체에 닿았을 때
    {
        AudioSource.PlayClipAtPoint(sound, transform.position);  //사운드 파일을 transform.position의 위치에서 재생
        if (col.gameObject.tag == "WALL") //닿은 물체의 태그가 WALL이라면
        {
            Destroy(col.gameObject);  //벽을 없앰
        }
        else if(col.gameObject.tag=="Enemy")
        {
            Score.Hit++;
            Debug.Log("enemy");
            if (Score.Hit > 5)
            {
                //enemy제거
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
                SceneManager.LoadScene("Lose");
            }
        }
        
        Destroy(gameObject);  //총알을 없앰
    }
}
