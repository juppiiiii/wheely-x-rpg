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
        "모험을 떠나고 있는 중 사람들이 쓰러져 있고 수상한 기운이 느껴진다... ",
        "대체 무슨일이지",
        "한번 주변을 돌아봐야겠다.",
        "돌아본 순간 몸이 굳어버렸다.",
        "다양한 몬스터들이 주변애 포진되어있어 숨을 죽이고 쳐다보고있었는데...",
        "아니 어떻게 알아봤지? 나한테 다가온다 살아야한다 반드시!"
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

        
        SceneManager.LoadScene("4.Game_Scene"); // �� �̸��� ������ �����ϼ���

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
