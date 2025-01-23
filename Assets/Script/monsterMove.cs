using UnityEngine;

public class monsterMove : MonoBehaviour
{
    float walktime = 0f;
    float stopwalk = 2.1f;
    float moveDuration = 0.5f; // 0.5�� ���� �̵�
    bool isMoving = true; // �̵� ������ ����
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
        if (isMoving) // �̵� ���� ���� �̵�
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
            isTileMoving = true; // MonsterTile�� ������ �̵� ����
            isMoving = false; // �̵��� ����
        }
    }
}
