using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] menuButtons; // 메뉴 버튼 배열
    private int selectedIndex = 0; // 현재 선택된 인덱스
    private int keyPressCount = 0; // 키 누른 횟수 카운터
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Menu Buttons Count: " + menuButtons.Length);
        selectedIndex = 1; // 이어하기 버튼의 인덱스에 맞게 수정
        UpdateMenuSelection();

  
    }

    // Update is called once per frame
    void Update()
    {

        // 위쪽 화살표 키 입력
        if (Input.GetKeyDown(KeyCode.O)) // 위로 이동
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.L)) // 아래로 이동
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            UpdateMenuSelection();
        }

        // 'a' 또는 's' 키 입력
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            keyPressCount++; // 카운터 증가
            Debug.Log(keyPressCount);
            // 4번 누르면 선택된 버튼 클릭
            if (keyPressCount >= 4)
            {
                menuButtons[selectedIndex].onClick.Invoke(); // 선택된 버튼 클릭
                keyPressCount = 0; // 카운터 초기화
                
            }
            
        }
    }

    void UpdateMenuSelection()
    {
        // 모든 버튼의 하이라이트 상태 초기화
        foreach (Button button in menuButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white; // 기본 색상
            colors.highlightedColor = Color.cyan; // 마우스 오버 시 색상
            colors.pressedColor = Color.green; // 클릭 시 색상
            button.colors = colors;
        }

        // 선택된 버튼 하이라이트
        ColorBlock selectedColors = menuButtons[selectedIndex].colors;
        selectedColors.normalColor = Color.yellow; // 하이라이트 색상
        selectedColors.highlightedColor = Color.red; // 선택된 버튼 마우스 오버 시 색상
        selectedColors.pressedColor = Color.magenta; // 선택된 버튼 클릭 시 색상
        menuButtons[selectedIndex].colors = selectedColors;
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("story_Scene");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
