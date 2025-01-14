using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveDistance = 1f;     // 이동할 거리(한 칸)

    private Vector2 mapMinBounds = new Vector2(-10f, -10f); // Define the minimum bounds of the map
    private Vector2 mapMaxBounds = new Vector2(10f, 10f);   // Define the maximum bounds of the map
    
    private bool isMoving = false;

    // q, a, o, l 각각 몇 번 눌렀는지 기록
    private int pressCountQ = 0;
    private int pressCountO = 0;
    private int pressCountA = 0;
    private int pressCountL = 0;

    // 몇 번 누르면 이동할지 (여기서는 4번 누르면 이동)
    private readonly int requiredPressCount = 4;

    void Update()
    {
        if (isMoving) return;  // 이미 이동 중이라면 입력받지 않음

        CheckKeyInput();
    }

    // 키 입력을 확인하는 메서드
    private void CheckKeyInput()
    {
        // 왼쪽 이동(Q) - 4번 누르면 이동
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressCountQ++;
            if (pressCountQ >= requiredPressCount)
            {
                Move(Vector2.left);
                pressCountQ = 0; // 카운트 초기화
            }
        }

        // 오른쪽 이동(O) - 4번 누르면 이동
        if (Input.GetKeyDown(KeyCode.O))
        {
            pressCountO++;
            if (pressCountO >= requiredPressCount)
            {
                Move(Vector2.right);
                pressCountO = 0; // 카운트 초기화
            }
        }

        // 아래 두 키(A, L)는 향후 로직을 추가할 예정이므로 구조만 만들어 둠
        if (Input.GetKeyDown(KeyCode.A))
        {
            pressCountA++;
            // TODO: 나중에 A 키 관련 로직 추가
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            pressCountL++;
            // TODO: 나중에 L 키 관련 로직 추가
        }
    }

    // 실제 이동 처리(여기서는 누적된 입력이 조건을 만족하면 한 칸 즉시 이동)
    private void Move(Vector2 direction)
    {
        isMoving = true;

        // moveDistance만큼 즉시 이동
        Vector3 targetPosition = transform.position + (Vector3)direction * moveDistance;
        transform.position = targetPosition;

        // 다른 방식(부드러운 이동 등)을 원한다면 코루틴(Lerp), 트윈 등을 고려
        isMoving = false;
    }
}