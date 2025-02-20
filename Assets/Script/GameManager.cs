using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ObjectManager objectManager;
    private UnityEngine.Tilemaps.Tilemap playerTilemap;

    // 웨이브 관련 변수
    [Header("Wave Settings")]
    public int currentWave = 1;           // 현재 웨이브 번호, TODO: 플레이어 데이터에서 로드
    public float waveDuration = 180f;     // 웨이브당 시간(초), TODO: 추후 180초(3분)으로 변경
    private float waveTimer = 0f;         // 웨이브 경과 시간
    private bool isWaveActive = false;    // 현재 웨이브가 진행 중인지 여부
    private bool isWaveCleared = false;   // 현재 웨이브 클리어 여부(예: 적을 모두 처치)

    // 웨이브 사이 준비 단계(인터벌) 관련
    public float waveInterval = 5f;        // 웨이브 종료 후 다음 웨이브 시작까지 대기 시간
    private float intervalTimer = 0f;       // 준비 단계(인터벌) 경과 시간
    private bool isIntervalActive = false;  // 웨이브와 웨이브 사이의 준비 단계가 진행 중인지 여부

    private bool isBossStage = false;     // 보스전 진입 여부

    // 턴 관련 변수
    [Header("Turn Settings")]
    public float turnDuration = 10f;      // 한 턴당 기본 시간
    private float turnTimer = 0f;         // 현재 턴 경과 시간
    private bool isTurnActive = false;    // 현재 턴 진행 중 여부
    
    // 행동 게이지 관련 변수
    [Header("Action Gauge")]
    public float maxActionGauge = 100f;   // 최대 행동 게이지
    public float currentActionGauge = 0f; // 현재 행동 게이지
    public float chargeRate = 20f;        // 초당 충전량 (5초 완충 = 100/5 = 20)
    private bool isCharging = false;      // 충전 중 여부
    private float lastInputTime = 0f;     // 마지막 입력 시간
    private const float INPUT_THRESHOLD = 1f; // 입력 감지 임계값

    // 행동 상태
    private enum ActionState
    {
        Idle,
        Charging,
        ReadyToAct,
        Acting
    }
    private ActionState currentState = ActionState.Idle;

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
        // 저장된 데이터 불러오기
        DataManager.instance.loadData();

        // 저장된 currentWave 적용
        currentWave = DataManager.instance.nowPlayer.currentWave;

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

        // 현재 웨이브 값을 저장
        DataManager.instance.nowPlayer.currentWave = currentWave;
        DataManager.instance.saveData();

        // 10웨이브까지는 인터벌 시작
        if (currentWave < 10)
        {
            StartInterval();
        }
        else
        {
            StartBossStage();
        }

    }

    // 웨이브와 웨이브 사이 준비 단계(인터벌) 시작
    private void StartInterval()
    {
        isIntervalActive = true;
        intervalTimer = 0f;
        Debug.Log("인터벌 시작(준비 단계)...");
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
            // 웨이브 타이머 업데이트
            waveTimer += Time.deltaTime;

            // 턴 시스템 업데이트
            UpdateTurnSystem();

            // 웨이브 종료 조건 체크
            if (waveTimer >= waveDuration)
            {
                isWaveCleared = true;
            }

            if (isWaveCleared)
            {
                EndWave();
            }
        }
        else if (isIntervalActive)
        {
            // 웨이브가 종료되고 인터벌(준비 단계) 중일 때
            intervalTimer += Time.deltaTime;

            if (intervalTimer >= waveInterval)
            {
                // 인터벌 종료 후 다음 웨이브 시작
                currentWave++;
                StartWave(currentWave);
            }
        }
    }

    private void UpdateTurnSystem()
    {
        if (!isTurnActive)
        {
            StartNewTurn();
            return;
        }

        turnTimer += Time.deltaTime;
        
        // 입력 감지 및 행동 게이지 처리
        if (CheckForContinuousInput())
        {
            if (currentState == ActionState.Idle)
            {
                currentState = ActionState.Charging;
            }

            if (currentState == ActionState.Charging)
            {
                ChargeActionGauge();
            }
        }
        else
        {
            // 입력이 없으면 충전 중지
            isCharging = false;
        }

        // 턴 종료 조건 체크
        if (turnTimer >= turnDuration)
        {
            EndTurn();
        }
    }

    private void StartNewTurn()
    {
        isTurnActive = true;
        turnTimer = 0f;
        currentActionGauge = 0f;
        currentState = ActionState.Idle;
        Debug.Log("새로운 턴 시작!");
    }

    private void EndTurn()
    {
        isTurnActive = false;
        isCharging = false;
        currentState = ActionState.Idle;
        Debug.Log("턴 종료!");
    }

    private bool CheckForContinuousInput()
    {
        // 키보드 입력 감지
        bool hasInput = Input.anyKey;
        
        if (hasInput)
        {
            lastInputTime = Time.time;
            return true;
        }

        // 마지막 입력으로부터 임계값 이내인지 확인
        return Time.time - lastInputTime < INPUT_THRESHOLD;
    }

    private void ChargeActionGauge()
    {
        if (currentActionGauge < maxActionGauge)
        {
            currentActionGauge += chargeRate * Time.deltaTime;
            
            // 게이지가 다 찼을 때
            if (currentActionGauge >= maxActionGauge)
            {
                currentActionGauge = maxActionGauge;
                currentState = ActionState.ReadyToAct;
                Debug.Log("행동 게이지 완충! 행동 가능!");
            }
        }
    }

    // 행동 실행 함수 (공격/방어)
    public void ExecuteAction(string actionType)
    {
        if (currentState != ActionState.ReadyToAct) return;

        switch (actionType.ToLower())
        {
            case "attack":
                Debug.Log("공격 행동 실행!");
                break;
            case "defend":
                Debug.Log("방어 행동 실행!");
                break;
        }

        // 행동 후 게이지 리셋
        currentActionGauge = 0f;
        currentState = ActionState.Idle;
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