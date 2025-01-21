using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkStory : MonoBehaviour
{
    public Transform player;
    public GameObject talkImage;
    public Text talkText;
    public static Vector2 lastTalkPartPosition; // ������ Talk_Part ��ġ ����


    public string[] textTool;
// Start is called before the first frame update
    void Start()
    {
        textTool = new string[] { 
            "�ȳ�  ",
            "��������",
            "���̻� ���� ����.",
            "������ ���� ����!",
            "fourtalking",
            "fivetalking"
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Talk_Part"))  // Ư�� �±׷� Ȯ��
        {
            
            lastTalkPartPosition = collider.transform.position;

            string objectName = collider.gameObject.name;
            collider.gameObject.SetActive(false);
            int index = int.Parse(objectName);
            Debug.Log(index+ " " +textTool[index]);

            talkImage.SetActive(true);
            talkText.gameObject.SetActive(true);
            talkText.text = "";
            StartCoroutine(Talk_Girl(index));

      
        }
    }

    IEnumerator Talk_Girl(int index)
    {
        // �ѱ��ھ� --
        string printtext = textTool[index];
        int count = 0;
        while (count < printtext.Length-1)
        {
            talkText.text += printtext[count];
            Debug.Log(talkText.text);
            count++;
            yield return new WaitForSeconds(0.1f);
        }
        // ��� --

        yield return new WaitForSeconds(1f);
        talkImage.SetActive(false);
        talkText.gameObject.SetActive(false);
    }



}
