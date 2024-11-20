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
            color.a = 0; // ó���� �����ϰ� ����
            hiddenObjectRenderer.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && glowCoroutine != null)
        {
            // Space�� ������ �� ������ ������Ʈ ���� �ڷ�ƾ ����
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
            // ������ �ڷ�ƾ�� �ִٸ� ����
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
            }

            // Glow �ڷ�ƾ ����
            glowCoroutine = StartCoroutine(Glow());
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Glow ȿ�� ����
            if (glowCoroutine != null)
            {
                StopCoroutine(glowCoroutine);
                glowCoroutine = null;
            }

            // ���� ���� ����
            spriteRenderer.color = Color.white;
        }
    }
    private IEnumerator Glow()
    {
        float duration = 1f; // �� ���� ��¦�ӿ� �ɸ��� �ð�
        while (true)
        {
            // ���� �������
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                spriteRenderer.color = Color.Lerp(Color.black, Color.yellow, normalizedTime);
                yield return null;
            }

            // ���� ��ο�����
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
        float duration = 2f; // õõ�� �����ϴ� �ð�
        Color color = renderer.color;

        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t / duration); // ���� �� 0���� 1�� ��ȭ
            renderer.color = color;
            yield return null;
        }

        // ���� ���� ������ 1�� �ǵ��� ����
        color.a = 1;
        renderer.color = color;
    }
}
