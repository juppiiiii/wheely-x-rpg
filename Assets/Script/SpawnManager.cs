using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public int maxCount;
    public int enemyCount;
    public float spawnTime = 3f;
    public float curTime;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs; // �� ������ �迭
    private List<GameObject> enemyPool; // ������Ʈ Ǯ
    private bool[] spawnPointOccupied; // ���� ����Ʈ ���� ����

    void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        enemyPool = new List<GameObject>();
        spawnPointOccupied = new bool[spawnPoints.Length]; // ���� ����Ʈ ���� �ʱ�ȭ

        for (int i = 0; i < maxCount; i++)
        {
            // �������� �� ������ ����
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); // ��Ȱ��ȭ ���·� Ǯ�� �߰�
            enemyPool.Add(enemy);
        }
    }

    void Update()
    {
        if (curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            // ���� ����Ʈ�� �������� ���� ��쿡�� ���� ����
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

        // Ǯ���� ��Ȱ��ȭ�� �� ������Ʈ ã��
        GameObject enemy = GetPooledEnemy();
        if (enemy != null)
        {
            enemyCount++;
            enemy.transform.position = spawnPoints[spawnPointIndex].position;
            enemy.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            enemy.SetActive(true); // Ȱ��ȭ
            spawnPointOccupied[spawnPointIndex] = true; // �ش� ���� ����Ʈ ���� ���� ������Ʈ
        }
    }

    private GameObject GetPooledEnemy()
    {
        // ��Ȱ��ȭ�� �� ������Ʈ�� ��ȯ
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null; // ��Ȱ��ȭ�� ������Ʈ�� ������ null ��ȯ
    }

    // ���� �׾��� �� ȣ��Ǵ� �޼���
    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false); // �� ��Ȱ��ȭ
        enemyCount--;

        // ���� �׾��� �� ���� ����Ʈ ���� ������Ʈ
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (enemyPool[i] == enemy)
            {
                // ���� ����Ʈ�� �ٽ� ����� �� �ֵ��� ����
                // ���� ����Ʈ�� �ε����� ���� ���� ��ġ���� ã�� ������ �ʿ���
                // ���� ���, ���� �ʱ� ��ġ�� �����ϰ� �ش� ��ġ�� �ε����� ã�� ���
                // ���⼭�� ������ ���÷� 0�� ���� ����Ʈ�� ����
                spawnPointOccupied[0] = false; // ���� �׾����Ƿ� ���� ����Ʈ�� �ٽ� ����� �� �ֵ��� ����
                break;
            }
        }
    }
}
