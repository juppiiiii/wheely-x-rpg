using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("플레이어 설정")]
    [SerializeField] private string playerPrefabPath = "Prefabs/Player"; // Resources 폴더 내의 프리팹 경로
    private Transform groundTransform; // Ground의 Transform에 대한 참조

    private GameObject playerInstance;
    public static GameManager Instance { get; private set; }

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

    void Start()
    {
        InitializeGroundTransform();
        LoadAndSpawnPlayer();
    }

    private void InitializeGroundTransform()
    {
        // "Ground" 태그를 가진 오브젝트의 Transform을 찾음
        GameObject groundObject = GameObject.FindWithTag("Ground");
        if (groundObject != null)
        {
            groundTransform = groundObject.transform;
        }
        else
        {
            Debug.LogError("Ground 오브젝트를 찾을 수 없습니다. 'Ground' 태그가 설정되어 있는지 확인하세요.");
        }
    }

    private void LoadAndSpawnPlayer()
    {
        // Resources 폴더에서 Player 프리팹 로드
        GameObject playerPrefab = Resources.Load<GameObject>(playerPrefabPath);

        if (playerPrefab == null)
        {
            Debug.LogError("Player 프리팹을 로드할 수 없습니다. 'Assets/Resources/Prefabs/Player.prefab' 경로를 확인하세요.");
            return;
        }

        if (groundTransform == null)
        {
            Debug.LogError("Ground Transform이 할당되지 않았습니다. 'Ground' 태그가 설정되어 있는지 확인하세요.");
            return;
        }

        // Ground의 경계를 계산하여 Z 축의 가장 하단 위치를 찾음
        Renderer groundRenderer = groundTransform.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogError("Ground 오브젝트에 Renderer가 없습니다. 경계를 계산할 수 없습니다.");
            return;
        }

        Bounds groundBounds = groundRenderer.bounds;
        Vector3 spawnPosition = new Vector3(groundBounds.center.x-1.5f, groundBounds.max.y + 0.5f, groundBounds.min.z+0.5f);

        // 플레이어의 회전을 설정하여 XZ 평면에 수직으로 서 있도록 함
        Quaternion playerRotation = Quaternion.Euler(90, 0, 0); // X축을 기준으로 회전 설정

        // 계산된 위치와 회전으로 Player 프리팹 인스턴스화
        playerInstance = Instantiate(playerPrefab, spawnPosition, playerRotation);
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

    // Update is called once per frame
    void Update()
    {
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
