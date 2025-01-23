using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ObjectManager objectManager;
    private UnityEngine.Tilemaps.Tilemap playerTilemap;

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
        InitializePlayerTilemap();
        SpawnPlayer();
    }

    private void InitializePlayerTilemap()
    {
        GameObject playerTileObj = GameObject.FindGameObjectWithTag("PlayerTile");
        if (playerTileObj != null)
        {
            playerTilemap = playerTileObj.GetComponent<UnityEngine.Tilemaps.Tilemap>();
            if (playerTilemap == null)
            {
                Debug.LogError("PlayerTile의 Tilemap 컴포넌트를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.LogError("PlayerTile을 찾을 수 없습니다!");
        }
    }

    private void SpawnPlayer()
    {
        Debug.Log($"SpawnPlayer 시작 - ObjectManager: {(objectManager != null ? "있음" : "없음")}");
        
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager가 없어 플레이어를 스폰할 수 없습니다!");
            return;
        }

        // PlayerTile 찾기
        GameObject playerTileObj = GameObject.FindGameObjectWithTag("PlayerTile");
        if (playerTileObj == null)
        {
            Debug.LogError("PlayerTile을 찾을 수 없습니다!");
            return;
        }

        // Tilemap 컴포넌트 가져오기
        UnityEngine.Tilemaps.Tilemap tilemap = playerTileObj.GetComponent<UnityEngine.Tilemaps.Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Tilemap 컴포넌트를 찾을 수 없습니다!");
            return;
        }

        // 타일맵의 첫 번째 타일 위치 찾기
        Vector3Int firstTilePosition = new Vector3Int(-2, -11, 0); // Scene에서 확인된 첫 번째 타일 위치
        Vector3 worldPosition = tilemap.GetCellCenterWorld(firstTilePosition);
        worldPosition.y = 0.65f;

        Debug.Log($"플레이어 스폰 시도 - 위치: {worldPosition}");
        // ObjectManager를 통해 플레이어 생성
        GameObject player = objectManager.CreatePlayer(worldPosition, Quaternion.Euler(90, 0, 0));
        Debug.Log($"플레이어 스폰 결과: {(player != null ? "성공" : "실패")}");
    }

    public bool IsMovementPossible(Vector3 position)
    {
        if (playerTilemap == null) return false;

        // 월드 좌표를 타일맵 셀 좌표로 변환
        Vector3Int cellPosition = playerTilemap.WorldToCell(position);
        
        // 해당 위치에 타일이 있는지 확인
        return playerTilemap.HasTile(cellPosition);
    }

    void Update()
    {
        GameObject playerInstance = objectManager.PlayerInstance;
        if (playerInstance != null)
        {
            Vector3 playerPosition = playerInstance.transform.position;
            
            // // 현재 위치가 이동 가능한 타일인지 확인
            // if (!IsMovementPossible(playerPosition))
            // {
            //     // 이동 불가능한 위치라면 마지막으로 유효했던 위치로 되돌림
            //     playerInstance.transform.position = GetLastValidPosition(playerPosition);
            // }
        }
    }

    private Vector3 GetLastValidPosition(Vector3 currentPosition)
    {
        // 8방향으로 검사하여 가장 가까운 유효한 위치 찾기
        Vector3[] directions = {
            Vector3.left, Vector3.right, Vector3.up, Vector3.down,
            new Vector3(-1, 1), new Vector3(1, 1), new Vector3(-1, -1), new Vector3(1, -1)
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 testPosition = currentPosition + dir * 0.1f;
            if (IsMovementPossible(testPosition))
            {
                return testPosition;
            }
        }

        // 유효한 위치를 찾지 못한 경우 스폰 위치로 돌려보냄
        return new Vector3(-2, 1, 0);
    }
}
