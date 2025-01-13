using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int maxCount;
    public int enemyCount;
    public float spawnTime = 3f;
    public float curTime;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            SpawnEnemy(x);
        }
        curTime += Time.deltaTime;

    }
    public void SpawnEnemy(int spawnPointIndex)
    {
        curTime = 0;
        enemyCount++;

        int enemyIndex = Random.Range(0,enemies.Length);
        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

    }

}
