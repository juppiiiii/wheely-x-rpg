using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 1f; // 이동할 거리(한 칸)

    private Vector2 mapMinBounds = new Vector2(-10f, -10f); // 맵 최소 범위
    private Vector2 mapMaxBounds = new Vector2(10f, 10f);   // 맵 최대 범위
    
    private bool isMoving = false;

    // q, a, o, l 각각 몇 번 눌렀는지 기록
    private int pressCountQ = 0;
    private int pressCountO = 0;
    private int pressCountA = 0;
    private int pressCountL = 0;

    // 몇 번 누르면 이동할지 (4번 누르면 이동)
    private readonly int requiredPressCount = 4;

    private Tilemap tilemap; // 참조할 타일맵

    [Header("Status Settings")]
    [SerializeField] private float maxHealth = 100f;  // 최대 체력
    private float currentHealth;                      // 현재 체력
    [SerializeField] private float defense = 10f;     // 방어력

    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 1f;    // 기본 공격력
    [SerializeField] private float attackRange = 1f;      // 공격 범위 (1칸)
    private bool isDefending = false;                     // 방어 상태

    void Start()
    {
        GameObject tilemapObject = GameObject.FindWithTag("PlayerTile");
        if (tilemapObject != null)
        {
            tilemap = tilemapObject.GetComponent<Tilemap>();
        }
        else
        {
            Debug.LogError("PlayerTile 태그를 가진 타일맵을 찾을 수 없습니다!");
        }

        // 현재 체력을 최대 체력으로 초기화
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isMoving) return;

        CheckKeyInput();

        // 행동 게이지가 찼을 때 추가 입력 확인
        if (GameManager.Instance.currentState == GameManager.ActionState.ReadyToAct)
        {
            CheckActionInput();
        }
    }

    /// <summary>
    /// 키 입력을 확인하는 메서드
    /// </summary>
    private void CheckKeyInput()
    {
        // 왼쪽 이동(Q) - 4번 누르면 이동
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressCountQ++;
            switch (pressCountQ)
            {
                case 1:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, true, 0);
                    break;
                case 2:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, true, 2);
                    break;
                case 3:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, true, 3);
                    break;
                case 4:
                    Move(Vector2.left);
                    pressCountQ = 0; // 카운트 초기화
                    UIManager.Instance.RemoveArrow();
                    break;
            }
        }

        // 오른쪽 이동(O) - 4번 누르면 이동
        if (Input.GetKeyDown(KeyCode.O))
        {
            pressCountO++;
            switch (pressCountO)
            {
                case 1:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, false, 0);
                    break;
                case 2:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, false, 2);
                    break;
                case 3:
                    UIManager.Instance.ShowArrowAbovePlayer(gameObject, false, 3);
                    break;
                case 4:
                    Move(Vector2.right);
                    pressCountO = 0; // 카운트 초기화
                    UIManager.Instance.RemoveArrow();
                    break;
            }
        }

        // 아래 두 키(A, L)는 향후 로직을 추가할 예정
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressCountA++;
            // TODO: A 키 관련 로직 추가
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            pressCountL++;
            // TODO: L 키 관련 로직 추가
        }
    }

    /// 행동 게이지가 찼을 때의 입력을 확인하는 메서드
    private void CheckActionInput()
    {
        // A키로 공격
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        // L키로 방어
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Defend();
        }
    }

    /// 실제 이동 처리(누적된 입력이 조건을 만족하면 한 칸 즉시 이동)
    private void Move(Vector2 direction)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance가 없습니다!");
            return;
        }

        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int((int)direction.x, (int)direction.y, 0);

        Vector3 targetPosition = tilemap.GetCellCenterWorld(targetCell);
        targetPosition.y = 0.65f;

        // 이동 가능한지 체크
        if (GameManager.Instance.IsMovementPossible(targetPosition))
        {
            isMoving = true;
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    /// 공격 실행 메서드
    private void Attack()
    {
        // 공격 실행 시 로그 출력
        Debug.Log("공격 실행!");
        GameManager.Instance.ExecuteAction("attack");

        /* 기존 몬스터 탐지 및 공격 로직은 주석 처리
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        bool hasAttacked = false;

        foreach (var hitCollider in hitColliders)
        {
            // Monster 태그를 가진 오브젝트 찾기
            if (hitCollider.CompareTag("Monster"))
            {
                MonsterStat monster = hitCollider.GetComponent<MonsterStat>();
                if (monster != null)
                {
                    // 몬스터와의 거리 계산 (2D 평면상에서)
                    Vector2 monsterPos2D = new Vector2(monster.transform.position.x, monster.transform.position.z);
                    Vector2 playerPos2D = new Vector2(transform.position.x, transform.position.z);
                    float distance = Vector2.Distance(monsterPos2D, playerPos2D);

                    // 정확히 1칸 거리에 있는 몬스터만 공격
                    if (Mathf.Approximately(distance, moveDistance))
                    {
                        monster.TakeDamage(attackDamage);
                        hasAttacked = true;
                        Debug.Log($"몬스터 공격! 거리: {distance}");
                    }
                }
            }
        }

        if (hasAttacked)
        {
            GameManager.Instance.ExecuteAction("attack");
        }
        else
        {
            Debug.Log("공격 범위 내에 몬스터가 없습니다!");
        }
        */
    }

    /// 방어 실행 메서드
    private void Defend()
    {
        isDefending = true;
        defense *= 2; // 방어력 2배 증가
        GameManager.Instance.ExecuteAction("defend");

        // 1턴 동안만 방어 상태 유지
        StartCoroutine(EndDefendNextTurn());
    }

    /// 다음 턴에 방어 상태를 해제하는 코루틴
    private IEnumerator EndDefendNextTurn()
    {
        yield return new WaitForSeconds(GameManager.Instance.turnDuration);
        isDefending = false;
        defense /= 2; // 방어력 원래대로 복구
        Debug.Log("방어 상태 해제");
    }

    /// 대미지를 받는 메서드 수정 (방어 상태 고려)
    public void TakeDamage(float damage)
    {
        float finalDamage = damage - defense;
        if (finalDamage < 0f) finalDamage = 0f;

        // 방어 상태일 때는 추가 대미지 감소
        if (isDefending)
        {
            finalDamage *= 0.5f; // 방어 상태에서는 대미지 50% 추가 감소
            Debug.Log("방어 상태: 대미지 감소!");
        }

        currentHealth -= finalDamage;
        Debug.Log($"플레이어가 {finalDamage} 피해를 받음. 남은 체력: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    /// 체력을 회복하는 메서드
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"플레이어가 {amount}만큼 회복. 현재 체력: {currentHealth}");
    }

    /// <summary>
    /// 사망 처리 (게임오버 등)
    /// </summary>
    private void Die()
    {
        Debug.Log("플레이어가 사망했습니다!");

        // GameObject 파괴 또는 게임 오버 화면 전환 등 원하는 로직 실행
        // 예) Destroy(gameObject);
        // 혹은
        // GameManager.Instance.GameOver();
    }
}