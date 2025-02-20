using UnityEngine;
using System.IO;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장

// 불러오는 방법
// 1.외부에 저장된 제이슨을 가져오기
// 2.제이슨을 데이터형태로 변환
// 3.불러온 데이터 사용

public class PlayerData
{
    //필요한 데이터 분류명 : 기초적인 스테이지만 저장하기로 했음
    public string name;
    public int stage;
    public int level;
    public int health;
    public int item;
    public int currentWave; // 현재 웨이브 저장
}
public class DataManager : MonoBehaviour
{
    //싱글톤 만들기
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData(); // 초깃값 플레이어 생성

    public string path;
    
    public int nowSlot;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (true)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion //#region , # endrigion 으로 코딩 영역 보이기 숨기기가능
        
        path = Application.persistentDataPath + "/save";
        print(path);
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    public void saveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void loadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
