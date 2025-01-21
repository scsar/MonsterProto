using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkStory : MonoBehaviour
{
    public Transform player;
    public GameObject talkImage;
    public Text talkText;
    public static Vector2 lastTalkPartPosition; // 마지막 Talk_Part 위치 저장


    public string[] textTool;
// Start is called before the first frame update
    void Start()
    {
        textTool = new string[] { 
            "안녕  ",
            "여긴어디지",
            "더이상 길이 없다.",
            "젤리를 만져 보자!",
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
        if (collider.gameObject.CompareTag("Talk_Part"))  // 특정 태그로 확인
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
        // 한글자씩 --
        string printtext = textTool[index];
        int count = 0;
        while (count < printtext.Length-1)
        {
            talkText.text += printtext[count];
            Debug.Log(talkText.text);
            count++;
            yield return new WaitForSeconds(0.1f);
        }
        // 재생 --

        yield return new WaitForSeconds(1f);
        talkImage.SetActive(false);
        talkText.gameObject.SetActive(false);
    }



}
