using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private GameObject arrowInstance;
    private SpriteRenderer arrowSpriteRenderer;
    private Sprite[] arrowSprites;

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

        // Arrow 스프라이트 배열 로드
        arrowSprites = Resources.LoadAll<Sprite>("Arrows");
        if (arrowSprites == null || arrowSprites.Length == 0)
        {
            Debug.LogError("Arrow 스프라이트를 로드할 수 없습니다!");
        }
    }

    public void ShowArrowAbovePlayer(GameObject player, bool isFlip, int stage = 0)
    {
        if (arrowInstance == null)
        {
            // 빈 게임오브젝트 생성
            arrowInstance = new GameObject("Arrows");
            arrowSpriteRenderer = arrowInstance.AddComponent<SpriteRenderer>();
        }

        // 스테이지에 따른 스프라이트 설정
        // if (stage >= 0 && stage < arrowSprites.Length)
        {
            arrowSpriteRenderer.sprite = arrowSprites[stage];
        }
        // else
        // {
        //     Debug.LogError($"잘못된 스테이지 번호입니다: {stage}");
        //     return;
        // }

        // Arrow의 위치를 플레이어의 위로 설정
        Vector3 arrowPosition = player.transform.position;
        arrowInstance.transform.localScale = new Vector3(
            player.transform.localScale.x,
            0.075f,
                            // arrowInstance.transform.localScale.y, 
                arrowInstance.transform.localScale.z
        );

        // Arrow Flip
        if (isFlip)
        {
            arrowInstance.transform.localScale = new Vector3(
                -Mathf.Abs(arrowInstance.transform.localScale.x), 
                arrowInstance.transform.localScale.y, 
                arrowInstance.transform.localScale.z
            );
        }
        else
        {
            arrowInstance.transform.localScale = new Vector3(
                Mathf.Abs(arrowInstance.transform.localScale.x), 
                arrowInstance.transform.localScale.y, 
                arrowInstance.transform.localScale.z
            );
        }

        arrowPosition.y += player.GetComponent<Renderer>().bounds.size.y;
        arrowInstance.transform.position = arrowPosition;
    }
    public void RemoveArrow()
    {
        if (arrowInstance != null)
        {
            Destroy(arrowInstance);
            arrowInstance = null;
        }
    }
}
