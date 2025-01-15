using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public int maxCount;
    public int enemyCount;
    public float spawnTime = 3f;
    public float curTime;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs; // 적 프리팹 배열
    private List<GameObject> enemyPool; // 오브젝트 풀
    private bool[] spawnPointOccupied; // 스폰 포인트 점유 상태

    void Start()
    {
        // 오브젝트 풀 초기화
        enemyPool = new List<GameObject>();
        spawnPointOccupied = new bool[spawnPoints.Length]; // 스폰 포인트 상태 초기화

        for (int i = 0; i < maxCount; i++)
        {
            // 랜덤으로 적 프리팹 선택
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); // 비활성화 상태로 풀에 추가
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        if (curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            // 스폰 포인트가 점유되지 않은 경우에만 적을 스폰
            if (!spawnPointOccupied[x])
            {
                SpawnEnemy(x);
            }
        }
        curTime += Time.deltaTime;
    }

    public void SpawnEnemy(int spawnPointIndex)
    {
        curTime = 0;

        // 풀에서 비활성화된 적 오브젝트 찾기
        GameObject enemy = GetPooledEnemy();
        if (enemy != null)
        {
            enemyCount++;
            enemy.transform.position = spawnPoints[spawnPointIndex].position;
            enemy.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            enemy.SetActive(true); // 활성화
            spawnPointOccupied[spawnPointIndex] = true; // 해당 스폰 포인트 점유 상태 업데이트
        }
    }

    private GameObject GetPooledEnemy()
    {
        // 비활성화된 적 오브젝트를 반환
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null; // 비활성화된 오브젝트가 없으면 null 반환
    }

    // 적이 죽었을 때 호출되는 메서드
    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false); // 적 비활성화
        enemyCount--;

        // 적이 죽었을 때 스폰 포인트 상태 업데이트
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (enemyPool[i] == enemy)
            {
                // 스폰 포인트를 다시 사용할 수 있도록 설정
                // 스폰 포인트의 인덱스를 적의 스폰 위치에서 찾는 로직이 필요함
                // 예를 들어, 적의 초기 위치를 저장하고 해당 위치의 인덱스를 찾는 방법
                // 여기서는 간단히 예시로 0번 스폰 포인트로 설정
                spawnPointOccupied[0] = false; // 적이 죽었으므로 스폰 포인트를 다시 사용할 수 있도록 설정
                break;
            }
        }
    }
}
