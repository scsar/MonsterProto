using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Coroutine glowCoroutine;
    public GameObject hiddenObject;
    private SpriteRenderer hiddenObjectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (hiddenObject != null)
        {
            hiddenObjectRenderer = hiddenObject.GetComponent<SpriteRenderer>();
            Color color = hiddenObjectRenderer.color;
            color.a = 0; // 처음에 투명하게 설정
            hiddenObjectRenderer.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && glowCoroutine != null)
        {
            // Space를 눌렀을 때 숨겨진 오브젝트 등장 코루틴 시작
            if (hiddenObjectRenderer != null)
            {
                StartCoroutine(FadeIn(hiddenObjectRenderer));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 기존의 코루틴이 있다면 중지
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }

            // Glow 코루틴 시작
            glowCoroutine = StartCoroutine(Glow());
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Glow 효과 중지
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
                glowCoroutine = null;
            }

            // 원래 색상 복구
            spriteRenderer.color = Color.white;
        }
    }
    private IEnumerator Glow()
    {
        float duration = 1f; // 한 번의 반짝임에 걸리는 시간
        while (true)
        {
            // 점점 밝아지게
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                spriteRenderer.color = Color.Lerp(Color.black, Color.yellow, normalizedTime);
                yield return null;
            }

            // 점점 어두워지게
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                spriteRenderer.color = Color.Lerp(Color.black, Color.white, normalizedTime);
                yield return null;
            }
        }
    }
    private IEnumerator FadeIn(SpriteRenderer renderer)
    {
        float duration = 2f; // 천천히 등장하는 시간
        Color color = renderer.color;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t / duration); // 알파 값 0에서 1로 변화
            renderer.color = color;
            yield return null;
        }

        // 알파 값이 완전히 1이 되도록 보장
        color.a = 1;
        renderer.color = color;
    }
}
