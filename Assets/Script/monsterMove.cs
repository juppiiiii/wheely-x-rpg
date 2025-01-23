using UnityEngine;

public class monsterMove : MonoBehaviour
{
    float walktime = 0f;
    float stopwalk = 2.1f;
    float moveDuration = 0.5f; // 0.5초 동안 이동
    bool isMoving = true; // 이동 중인지 여부
    bool isTileMoving = true;
    monsterStat monsterStat;

    void Start()
    {
        monsterStat = GetComponent<monsterStat>();
    }

    void Update()
    {
        move();
    }

    void move()
    {
        if (isMoving) // 이동 중일 때만 이동
        {
            Vector3 dir = new Vector3(0, 0, -1);
            transform.position += dir * monsterStat.moveSpeed * Time.deltaTime;
            walktime += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterTile"))
        {
            isTileMoving = true; // MonsterTile에 닿으면 이동 시작
            isMoving = false; // 이동을 멈춤
        }
    }
}
