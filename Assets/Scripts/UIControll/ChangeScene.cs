using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public static bool[] isclear = {true, false, false, false};
    public GameObject txt;
    public Image fadeImage;
    public GameObject mainMenuButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            mainMenuButton.SetActive(!mainMenuButton.activeSelf); // 현재 상태 반전
            exitButton.SetActive(!exitButton.activeSelf); // 현재 상태 반전

            if (mainMenuButton.activeSelf) {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;

            }

        }
    }

    public void ChangeSceneLoad(int number)
    {
        if (isclear[number-1])
        {
            StartCoroutine(FadeToBlack(number));
        }
        else
        {
            StartCoroutine(NoEnter());
        }   
    }

    private IEnumerator NoEnter()
    {
        txt.SetActive(true); // 활성화
        yield return new WaitForSeconds(2f); // seconds 만큼 대기
        txt.SetActive(false); // 비활성화
    }

    public void GoMain()
    {
        SceneManager.LoadScene(0);

    }

    IEnumerator FadeToBlack(int index)
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / 2);
            fadeImage.color = color;
            yield return null;
        }

        // 완전히 어두워진 상태를 보장합니다.
        color.a = 1f;
        fadeImage.color = color;
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
