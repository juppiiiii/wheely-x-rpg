using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] menuButtons; // �޴� ��ư �迭
    private int selectedIndex = 0; // ���� ���õ� �ε���
    private int keyPressCount = 0; // Ű ���� Ƚ�� ī����
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Menu Buttons Count: " + menuButtons.Length);
        selectedIndex = 1; // �̾��ϱ� ��ư�� �ε����� �°� ����
        UpdateMenuSelection();

  
    }

    // Update is called once per frame
    void Update()
    {

        // ���� ȭ��ǥ Ű �Է�
        if (Input.GetKeyDown(KeyCode.O)) // ���� �̵�
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.L)) // �Ʒ��� �̵�
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            UpdateMenuSelection();
        }

        // 'a' �Ǵ� 's' Ű �Է�
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            keyPressCount++; // ī���� ����
            Debug.Log(keyPressCount);
            // 4�� ������ ���õ� ��ư Ŭ��
            if (keyPressCount >= 4)
            {
                menuButtons[selectedIndex].onClick.Invoke(); // ���õ� ��ư Ŭ��
                keyPressCount = 0; // ī���� �ʱ�ȭ
                
            }
            
        }
    }

    void UpdateMenuSelection()
    {
        // ��� ��ư�� ���̶���Ʈ ���� �ʱ�ȭ
        foreach (Button button in menuButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white; // �⺻ ����
            colors.highlightedColor = Color.cyan; // ���콺 ���� �� ����
            colors.pressedColor = Color.green; // Ŭ�� �� ����
            button.colors = colors;
        }

        // ���õ� ��ư ���̶���Ʈ
        ColorBlock selectedColors = menuButtons[selectedIndex].colors;
        selectedColors.normalColor = Color.yellow; // ���̶���Ʈ ����
        selectedColors.highlightedColor = Color.red; // ���õ� ��ư ���콺 ���� �� ����
        selectedColors.pressedColor = Color.magenta; // ���õ� ��ư Ŭ�� �� ����
        menuButtons[selectedIndex].colors = selectedColors;
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("story_Scene");
    }

    public void OnClickLoad()
    {
        Debug.Log("�ҷ�����");
    }

    public void OnClickOption()
    {
        Debug.Log("�ɼ�");
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
