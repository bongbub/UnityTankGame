using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    //string을 담을 Queue를 선언
    public Queue<string> sentences;

    //string형 currentSentence 변수 선언
    public string currentSentence;

    //textMesh변수 선언 (사용하려면 TMPro 네임스페이스 추가하기)
    public TextMeshPro text;

    //Quad의 사이즈를 변화시키기 위해 GameObejct로 받아오기
    public GameObject quad;



    //해당 메서드가 호출되면 npc의 대사를 전달받아 큐에 저장함
    public void Ondialogue(string[]lines, Transform ChatPoint)
    {
        //시작할 때 한 번 chatBox 포지션을 Point의 포지션으로 초기화
        transform.position = ChatPoint.position;

        sentences = new Queue<string>();  //큐 초기화
        sentences.Clear();
        //foreach문으로 string 배열의 값을 전부 큐에 차례로 넣기
        foreach(var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(ChatPoint));
    }


    //큐에 담은 string을 코루틴으로 차례대로 말풍선으로 띄워주기
    IEnumerator DialogueFlow(Transform ChatPoint)
    {
        yield return null;
        //while문으로 큐의 개수만큼 반복해서 대사를 textMesh에 넣기
        while (sentences.Count>0) 
        {
            //큐의 데이터를 한 개씩 currentSentence에 담았다가
            //currentSentence를 TextMesh.text에 넣을 것임
            currentSentence = sentences.Dequeue();

            //TextMesh의 text에 대사 담기
            text.text = currentSentence;


            //TextMesh에 대사를 넣은 후 크기에 맞게 Quad 크기를 변화시키기
            //lacalScale로 말풍선의 크기를 계산하여 초기화
            float x = text.preferredWidth;
            //조건이 참이면 ?참이면 그거 반환 , :거짓이면 그거 반환
            x = (x > 3) ? 3 : x + 0.3f;  //말풍선의 width를 3으로 인스펙터창에서 정해줫으니 3으로 한거임

            quad.transform.localScale=new Vector2(x, text.preferredHeight + 0.3f);

            //말풍선의 크기가 초기화 된 후 크기에 맞춰서 다시 초기화
            transform.position = new Vector2(ChatPoint.position.x, ChatPoint.position.y + text.preferredHeight / 2);



            //한 마디 했으니까 잠깐 기다린 후 다음 대사 넣기
            yield return new WaitForSeconds(4f);
        }

        //큐의 대사를 다 말했다면 말풍선은 필요 없으니 제거
        Destroy(gameObject);
    }

}
