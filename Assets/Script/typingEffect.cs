using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class TypingEffect : MonoBehaviour
{
    public Text tx;
    public Image img; // �̹��� ������Ʈ �߰�
    private string[] m_texts = {
        "������ �ϴ���... �ɻ�ġ ���� ����� ������ �ֺ� ������� �������ִ�... ����������?",
        "�̰��� ����ü � ���ϱ�?",
        "�ֺ��� ������߰ڴ�.",
        "������ �ֽ��� ���� ���� ���� ������ȴ�.",
        "���͵�� ���� �����ư� �ѹ��ڱ��� ������ �ٰ����� �����Ѵ�.",
        "���� �������� ���ĳ������Ѵ�... �´�...!"
    };
    private int currentTextIndex = 0;

    void Start()
    {
        // ù ��° ���� Ÿ���� ����
        StartCoroutine(TypeText(m_texts[currentTextIndex]));
        ChangeImage();
    }

    private void Update()
    {
        // ���� ������ ���� �� ���� Ű �Է� �� ���� �������� �̵�
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S))
        {
            if (currentTextIndex < m_texts.Length - 1)
            {
                currentTextIndex++;
                ChangeImage(); // ��� ���� ���� �̹��� ����
                StartCoroutine(TypeText(m_texts[currentTextIndex]));
                
               
            }
            else
            {
                // ��� ������ ���� �� �̹��� ����
                ChangeImageAndContinue();
            }
        }
    }

    IEnumerator TypeText(string text)
    {
        tx.text = ""; // �ؽ�Ʈ �ʱ�ȭ

        for (int i = 0; i <= text.Length; i++)
        {
            tx.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void ChangeImageAndContinue()
    {
        // ���� ��� �ε����� ���� �̹��� ����
        ChangeImage();

        
        SceneManager.LoadScene("Alpha_2_Scene"); // �� �̸��� ������ �����ϼ���

    }

    void ChangeImage()
    {
        // �̹��� ���� ����
        if (currentTextIndex < 3) // 0, 1, 2 �ε����� �ش��ϴ� ���
        {
            img.sprite = Resources.Load<Sprite>("story_img1"); // ù ��° �̹���
        }
        else // 3, 4, 5 �ε����� �ش��ϴ� ���
        {
            img.sprite = Resources.Load<Sprite>("story_img2"); // �� ��° �̹���
        }
    }
}
