using System.CodeDom.Compiler;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject enemyBossPrefab;

    // 3명의 적오브젝트를 배열안에 넣기
    GameObject[] enemyA;
    GameObject[] enemyB;
    GameObject[] enemyC;
    GameObject[] Boss;

    GameObject[] targetPool;

    // 오브젝트 출현 숫자 정하기
    int createEnemy_a = 5;
    int createEnemy_b = 5;
    int createEnmey_c = 5;
    int createBoss = 1;

    [Header("프리팹 경로")]
    [SerializeField] private string playerPrefabPath = "Prefabs/Player";

    private GameObject playerInstance;
    public GameObject PlayerInstance => playerInstance;

    void Awake()
    {
        enemyA = new GameObject[createEnemy_a];
        enemyB = new GameObject[createEnemy_b];
        enemyC = new GameObject[createEnmey_c];
        Boss = new GameObject[createBoss];

        Generate();
    }

    void Generate()
    {
        // #.적 A,B,C,BOSS 포함
        for(int index = 0; index < enemyA.Length; index++)
        {
            enemyA[index] = Instantiate(enemyAPrefab);
            enemyA[index].SetActive(false);
        }

        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }

        for (int index = 0; index < enemyC.Length; index++)
        {
            enemyC[index] = Instantiate(enemyCPrefab);
            enemyC[index].SetActive(false);
        }

        for (int index = 0; index < Boss.Length; index++)
        {
            Boss[index] = Instantiate(enemyBossPrefab);
            Boss[index].SetActive(false);
        }
             
    }
    public GameObject MakeObj(string type)
    {
        
        switch (type)
        {
            case "EnemyA":
                targetPool = enemyA;
                break;

            case "EnemyB":
                targetPool = enemyB;
                break;

            case "EnemyC":
                targetPool = enemyC;
                break;

            case "Boss":
                targetPool = Boss;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject CreatePlayer(Vector3 position, Quaternion rotation)
    {        
        // Resources 폴더에서 플레이어 프리팹 로드
        GameObject playerPrefab = Resources.Load<GameObject>(playerPrefabPath);
        
        if (playerPrefab == null)
        {
            Debug.LogError($"플레이어 프리팹을 찾을 수 없습니다: {playerPrefabPath}");
            return null;
        }

        // Ground의 경계를 계산하여 Z 축의 가장 하단 위치를 찾음
        GameObject ground = GameObject.Find("Ground");
        if (ground == null)
        {
            Debug.LogError("Ground 오브젝트를 찾을 수 없습니다!");
            return null;
        }

        Renderer groundRenderer = ground.GetComponent<Renderer>();
        if (groundRenderer == null)
        {
            Debug.LogError("Ground 오브젝트에 Renderer가 없습니다. 경계를 계산할 수 없습니다.");
            return null;
        }

        Bounds groundBounds = groundRenderer.bounds;
        Vector3 spawnPosition = new Vector3(groundBounds.center.x-1.5f, groundBounds.max.y + 0.5f, groundBounds.min.z+0.5f);

        // 플레이어의 회전을 설정하여 XZ 평면에 수직으로 서 있도록 함
        Quaternion playerRotation = Quaternion.Euler(90, 0, 0); // X축을 기준으로 회전 설정

        // 플레이어 생성
        playerInstance = Instantiate(playerPrefab, spawnPosition, playerRotation);        
        return playerInstance;
    }

    // 플레이어 제거
    public void DestroyPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }
    }
}
