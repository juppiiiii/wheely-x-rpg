using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class TypingEffect : MonoBehaviour
{
    public Text tx;
    public Image img; // 이미지 컴포넌트 추가
    private string[] m_texts = {
        "여행을 하는중... 심상치 않은 기운이 감돌고 주변 사람들이 쓰러져있다... 무슨일이지?",
        "이곳은 도대체 어떤 곳일까?",
        "주변을 살펴봐야겠다.",
        "정면을 주시한 순간 나는 몸이 멈춰버렸다.",
        "몬스터들과 눈이 마주쳤고 한발자국씩 적들이 다가오기 시작한다.",
        "죽지 않으려면 헤쳐나가야한다... 온다...!"
    };
    private int currentTextIndex = 0;

    void Start()
    {
        // 첫 번째 문장 타이핑 시작
        StartCoroutine(TypeText(m_texts[currentTextIndex]));
        ChangeImage();
    }

    private void Update()
    {
        // 현재 문장이 끝난 후 엔터 키 입력 시 다음 문장으로 이동
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S))
        {
            if (currentTextIndex < m_texts.Length - 1)
            {
                currentTextIndex++;
                ChangeImage(); // 대사 변경 전에 이미지 변경
                StartCoroutine(TypeText(m_texts[currentTextIndex]));
                
               
            }
            else
            {
                // 모든 문장이 끝난 후 이미지 변경
                ChangeImageAndContinue();
            }
        }
    }

    IEnumerator TypeText(string text)
    {
        tx.text = ""; // 텍스트 초기화

        for (int i = 0; i <= text.Length; i++)
        {
            tx.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void ChangeImageAndContinue()
    {
        // 현재 대사 인덱스에 따라 이미지 변경
        ChangeImage();

        
        SceneManager.LoadScene("Alpha_2_Scene"); // 씬 이름을 적절히 변경하세요

    }

    void ChangeImage()
    {
        // 이미지 변경 로직
        if (currentTextIndex < 3) // 0, 1, 2 인덱스에 해당하는 경우
        {
            img.sprite = Resources.Load<Sprite>("story_img1"); // 첫 번째 이미지
        }
        else // 3, 4, 5 인덱스에 해당하는 경우
        {
            img.sprite = Resources.Load<Sprite>("story_img2"); // 두 번째 이미지
        }
    }
}
