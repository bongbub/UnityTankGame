using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    //string�� ���� Queue�� ����
    public Queue<string> sentences;

    //string�� currentSentence ���� ����
    public string currentSentence;

    //textMesh���� ���� (����Ϸ��� TMPro ���ӽ����̽� �߰��ϱ�)
    public TextMeshPro text;

    //Quad�� ����� ��ȭ��Ű�� ���� GameObejct�� �޾ƿ���
    public GameObject quad;



    //�ش� �޼��尡 ȣ��Ǹ� npc�� ��縦 ���޹޾� ť�� ������
    public void Ondialogue(string[]lines, Transform ChatPoint)
    {
        //������ �� �� �� chatBox �������� Point�� ���������� �ʱ�ȭ
        transform.position = ChatPoint.position;

        sentences = new Queue<string>();  //ť �ʱ�ȭ
        sentences.Clear();
        //foreach������ string �迭�� ���� ���� ť�� ���ʷ� �ֱ�
        foreach(var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(ChatPoint));
    }


    //ť�� ���� string�� �ڷ�ƾ���� ���ʴ�� ��ǳ������ ����ֱ�
    IEnumerator DialogueFlow(Transform ChatPoint)
    {
        yield return null;
        //while������ ť�� ������ŭ �ݺ��ؼ� ��縦 textMesh�� �ֱ�
        while (sentences.Count>0) 
        {
            //ť�� �����͸� �� ���� currentSentence�� ��Ҵٰ�
            //currentSentence�� TextMesh.text�� ���� ����
            currentSentence = sentences.Dequeue();

            //TextMesh�� text�� ��� ���
            text.text = currentSentence;


            //TextMesh�� ��縦 ���� �� ũ�⿡ �°� Quad ũ�⸦ ��ȭ��Ű��
            //lacalScale�� ��ǳ���� ũ�⸦ ����Ͽ� �ʱ�ȭ
            float x = text.preferredWidth;
            //������ ���̸� ?���̸� �װ� ��ȯ , :�����̸� �װ� ��ȯ
            x = (x > 3) ? 3 : x + 0.3f;  //��ǳ���� width�� 3���� �ν�����â���� ���آZ���� 3���� �Ѱ���

            quad.transform.localScale=new Vector2(x, text.preferredHeight + 0.3f);

            //��ǳ���� ũ�Ⱑ �ʱ�ȭ �� �� ũ�⿡ ���缭 �ٽ� �ʱ�ȭ
            transform.position = new Vector2(ChatPoint.position.x, ChatPoint.position.y + text.preferredHeight / 2);



            //�� ���� �����ϱ� ��� ��ٸ� �� ���� ��� �ֱ�
            yield return new WaitForSeconds(4f);
        }

        //ť�� ��縦 �� ���ߴٸ� ��ǳ���� �ʿ� ������ ����
        Destroy(gameObject);
    }

}
