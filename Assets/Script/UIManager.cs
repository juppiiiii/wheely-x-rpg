using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private GameObject arrowInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowArrowAbovePlayer(GameObject player, bool isFlip)
    {
        // Arrow 프리팹 로드
        GameObject arrowPrefab = Resources.Load<GameObject>("Prefabs/Arrow");
        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow 프리팹을 로드할 수 없습니다!");
            return;
        }

        // Arrow 인스턴스 생성
        arrowInstance = Instantiate(arrowPrefab);


        // // 플레이어의 크기 가져오기
        // float playerWidth = player.GetComponent<Renderer>().bounds.size.x;
        // Debug.Log($"플레이어의 너비: {playerWidth}");

        // // Arrow의 크기를 플레이어의 너비에 맞춤
        // arrowInstance.transform.localScale = new Vector3(playerWidth, arrowInstance.transform.localScale.y, arrowInstance.transform.localScale.z);
        // Debug.Log($"Arrow의 너비: {arrowInstance.GetComponent<Renderer>().bounds.size.x}");

        // Arrow의 위치를 플레이어의 위로 설정
        Vector3 arrowPosition = player.transform.position;

        // Arrow Flip
        if (isFlip)
        {
            arrowInstance.transform.localScale = new Vector3(
                -arrowInstance.transform.localScale.x, 
                arrowInstance.transform.localScale.y, 
                arrowInstance.transform.localScale.x
            );
        }

        arrowPosition.y += player.GetComponent<Renderer>().bounds.size.y;
        arrowInstance.transform.position = arrowPosition;
    }
    public void RemoveArrow()
    {
        Debug.Log("Current Arrow Instance: " + arrowInstance);
        if (arrowInstance != null)
        {
            Destroy(arrowInstance);
            arrowInstance = null;
            Debug.Log("Arrow removed");
        }
        else
        {
            Debug.Log("No arrow to remove");
        }
    }
}
