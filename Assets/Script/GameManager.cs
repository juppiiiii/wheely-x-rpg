using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ObjectManager objectManager;
    private UnityEngine.Tilemaps.Tilemap playerTilemap;

    // 웨이브 관련 변수
    [Header("Wave Settings")]
    public int currentWave = 1;           // 현재 웨이브 번호, TODO: 플레이어 데이터에서 로드
    public float waveDuration = 10f;     // 웨이브당 시간(초), TODO: 추후 180초(3분)으로 변경
    private float waveTimer = 0f;         // 웨이브 경과 시간
    private bool isWaveActive = false;    // 현재 웨이브가 진행 중인지 여부
    private bool isWaveCleared = false;   // 현재 웨이브 클리어 여부(예: 적을 모두 처치)

    private bool isBossStage = false;     // 보스전 진입 여부

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

        // 첫 웨이브 시작
        StartWave(currentWave);
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

    // 웨이브 시작
    private void StartWave(int waveNumber)
    {
        isWaveActive = true;
        isWaveCleared = false;
        waveTimer = 0f;

        Debug.Log($"Wave {waveNumber} 시작!");

        // 웨이브에 따른 스폰 로직 추가(예: 적 스폰)
        // 예: objectManager.CreateEnemy(...); 등등
    }

    // 웨이브 종료 시 다음 웨이브로
    private void EndWave()
    {
        isWaveActive = false;
        Debug.Log($"Wave {currentWave} 종료!");

        // 다음 웨이브로 넘어가거나 보스 웨이브로 진입
        currentWave++;
        if (currentWave <= 10)
        {
            StartWave(currentWave);
        }
        else
        {
            // 10웨이브 후 보스 스테이지
            StartBossStage();
        }
    }

    // 보스전 시작
    private void StartBossStage()
    {
        isBossStage = true;
        Debug.Log("보스 스테이지 진입!");

        // TODO: 보스 소환, 씬 전환, UI 변경 등 보스전 로직을 구현
        // 예: objectManager.CreateBoss(...);
    }

    // Update 내에서 웨이브 타이머 및 클리어 조건 체크
    void Update()
    {   
        // 현재 웨이브 및 진행 시간 출력
        Debug.Log($"Wave {currentWave} - 진행 시간: {waveTimer:F1} / {waveDuration:F1}");


        // TODO: 플레이어 Tilemap 검사 로직(원본)
        GameObject playerInstance = objectManager.PlayerInstance;
        if (playerInstance != null)
        {
            Vector3 playerPosition = playerInstance.transform.position;
            // 이동 불가능한 위치 체크 (주석 처리된 부분 참고)
        }

        // 보스 스테이지라면 여기서 별도 로직 처리
        if (isBossStage)
        {
            // 보스 체력 체크, 보스 스테이지 종료 처리 등
            return;
        }

        // 웨이브 진행 중이라면 시간 체크
        if (isWaveActive)
        {
            waveTimer += Time.deltaTime;

            // 웨이브 시간(3분) 초과 혹은 적 처치 등으로 클리어 조건만 맞으면 isWaveCleared = true 처리
            // 여기서는 예시로 3분이 지나면 자동 웨이브 클리어로 가정
            if (waveTimer >= waveDuration)
            {
                isWaveCleared = true;
            }

            // 웨이브 클리어 조건을 만족하면 웨이브 종료
            if (isWaveCleared)
            {
                EndWave();
            }
        }
    }

    // 이동 가능 여부 체크
    public bool IsMovementPossible(Vector3 position)
    {
        if (playerTilemap == null) return false;

        // 월드 좌표를 타일맵 셀 좌표로 변환
        Vector3Int cellPosition = playerTilemap.WorldToCell(position);

        // 해당 위치에 타일이 있는지 확인
        return playerTilemap.HasTile(cellPosition);
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