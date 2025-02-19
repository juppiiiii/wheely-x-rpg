using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class select : MonoBehaviour
{
    
    public GameObject slotCreat;
    public TextMeshProUGUI[] slotText;
    public Image[] slotImages; // 슬롯 버튼의 Image 컴포넌트 배열
    public TextMeshProUGUI newPlayerName;
    public TMP_InputField inputField; // 새로운 이름을 입력할 InputField
    public Button confirmButton; // 확인 버튼
    private int selectedIndex = 0; // 현재 선택된 슬롯 인덱스
    private int keyPressCount = 0; // 키 누른 횟수 카운터
    bool[] savefile = new bool[3];

    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.loadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();
        UpdateSlotSelection(); // 슬롯 선택 상태 업데이트
        confirmButton.interactable = false; // 시작할 때 버튼 비활성화
    }

    void Update()
    {
        // 위쪽 화살표 키 입력
        if (Input.GetKeyDown(KeyCode.O)) // 위로 이동
        {
            selectedIndex = (selectedIndex - 1 + slotText.Length) % slotText.Length;
            UpdateSlotSelection();
        }
        else if (Input.GetKeyDown(KeyCode.L)) // 아래로 이동
        {
            selectedIndex = (selectedIndex + 1) % slotText.Length;
            UpdateSlotSelection();
        }

        // 'a' 또는 's' 키 입력
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            keyPressCount++; // 카운터 증가
                             // 4번 누르면 선택된 슬롯 클릭
            if (keyPressCount >= 4)
            {
                if (slotCreat.activeSelf) // slotCreat가 활성화되어 있을 때
                {
                    confirmButton.onClick.Invoke(); // 버튼 클릭
                }
                else
                {
                    slot(selectedIndex); // 선택된 슬롯 클릭
                }
                keyPressCount = 0; // 카운터 초기화
            }
        }

        // 'Q'와 'A' 키가 동시에 눌렸는지 확인
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.A))
        {
            confirmButton.interactable = true; // 버튼 활성화
                                               // 버튼 클릭을 즉시 수행
            confirmButton.onClick.Invoke(); // 버튼 클릭
        }
        else
        {
            confirmButton.interactable = false; // 버튼 비활성화
        }
    }

    public void slot(int num)
    {
        DataManager.instance.nowSlot = num;

        if (savefile[num])
        {
            // 저장된 데이터가 있을 때
            DataManager.instance.loadData();
            goGame();
        }
        else
        {
            // 저장된 데이터가 없을 때
            Create();
        }
    }

    public void Create()
    {
        slotCreat.gameObject.SetActive(true);
        inputField.Select(); // InputField에 포커스 주기
        inputField.ActivateInputField(); // InputField 활성화
    }

    public void goGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.saveData();
        }

        SceneManager.LoadScene("4.Game_Scene");
    }

    void UpdateSlotSelection()
    {
        // 모든 슬롯의 하이라이트 상태 초기화
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].color = Color.white; // 기본 색상
        }

        // 선택된 슬롯 하이라이트
        slotImages[selectedIndex].color = Color.yellow; // 하이라이트 색상
    }
}
