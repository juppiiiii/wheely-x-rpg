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

    public GameObject playerInstance;
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

        // PlayerTile 태그를 가진 Tilemap을 찾음
        GameObject playerTile = GameObject.FindGameObjectWithTag("PlayerTile");
        if (playerTile == null)
        {
            Debug.LogError("PlayerTile 태그를 가진 Tilemap을 찾을 수 없습니다!");
            return null;
        }

        Renderer tileRenderer = playerTile.GetComponent<Renderer>();
        if (tileRenderer == null)
        {
            Debug.LogError("PlayerTile 오브젝트에 Renderer가 없습니다. 경계를 계산할 수 없습니다.");
            return null;
        }

        // 플레이어 생성
        playerInstance = Instantiate(playerPrefab, position, rotation);

        // 플레이어 스케일 조정
        playerInstance.transform.localScale *= 1.5f;

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
