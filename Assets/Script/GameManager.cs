using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ObjectManager objectManager;
    private Transform groundTransform;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // ObjectManager 찾기
            objectManager = FindAnyObjectByType<ObjectManager>();
            if (objectManager == null)
            {
                Debug.LogError("씬에서 ObjectManager를 찾을 수 없습니다!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGroundTransform();
        SpawnPlayer();
    }

    private void InitializeGroundTransform()
    {
        GameObject ground = GameObject.Find("Ground");
        Debug.Log($"Ground 오브젝트 찾기 결과: {(ground != null ? "성공" : "실패")}");
        
        if (ground != null)
        {
            groundTransform = ground.transform;
            Debug.Log("Ground Transform이 설정되었습니다.");
        }
        else
        {
            Debug.LogError("Ground 오브젝트를 찾을 수 없습니다!");
        }
    }

    private void SpawnPlayer()
    {
        Debug.Log($"SpawnPlayer 시작 - ObjectManager: {(objectManager != null ? "있음" : "없음")}, GroundTransform: {(groundTransform != null ? "있음" : "없음")}");
        
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager가 없어 플레이어를 스폰할 수 없습니다!");
            return;
        }

        if (groundTransform == null)
        {
            Debug.LogError("Ground가 없어 플레이어를 스폰할 수 없습니다!");
            return;
        }

        // 스폰 위치 계산
        Vector3 spawnPosition = groundTransform.position;
        spawnPosition.y = 1f; // 플레이어의 높이 조정
        Quaternion playerRotation = Quaternion.identity;

        Debug.Log($"플레이어 스폰 시도 - 위치: {spawnPosition}");
        // ObjectManager를 통해 플레이어 생성
        GameObject player = objectManager.CreatePlayer(spawnPosition, playerRotation);
        Debug.Log($"플레이어 스폰 결과: {(player != null ? "성공" : "실패")}");
    }

    // 특정 위치로 이동이 가능한지 체크하는 메서드
    public bool IsMovementPossible(Vector3 position)
    {
        if (groundTransform == null) return false;

        Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
        if (groundRenderer == null) return false;

        Bounds groundBounds = groundRenderer.bounds;
        
        // 이동하려는 위치가 경계 내에 있는지 확인
        return position.x >= groundBounds.min.x && 
                position.x <= groundBounds.max.x && 
                position.z >= groundBounds.min.z && 
                position.z <= groundBounds.max.z;
    }

    void Update()
    {
        GameObject playerInstance = objectManager.PlayerInstance;
        if (playerInstance != null && groundTransform != null)
        {
            // 바닥의 경계 가져오기
            Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
            if (groundRenderer != null)
            {
                Bounds groundBounds = groundRenderer.bounds;

                // 플레이어의 현재 위치 가져오기
                Vector3 playerPosition = playerInstance.transform.position;

                // 플레이어 위치를 바닥의 경계 내로 제한
                playerPosition.x = Mathf.Clamp(playerPosition.x, groundBounds.min.x, groundBounds.max.x);
                playerPosition.z = Mathf.Clamp(playerPosition.z, groundBounds.min.z, groundBounds.max.z);

                // 제한된 위치 적용
                playerInstance.transform.position = playerPosition;
            }
        }
    }
}
