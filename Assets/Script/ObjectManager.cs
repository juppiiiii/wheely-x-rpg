using System.CodeDom.Compiler;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;
    public GameObject enemyBossPrefab;

    // 3���� ��������Ʈ�� �迭�ȿ� �ֱ�
    GameObject[] enemyA;
    GameObject[] enemyB;
    GameObject[] enemyC;
    GameObject[] Boss;

    GameObject[] targetPool;

    // ������Ʈ ���� ���� ���ϱ�
    int createEnemy_a = 5;
    int createEnemy_b = 5;
    int createEnmey_c = 5;
    int createBoss = 1;

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
        // #.�� A,B,C,BOSS ����
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
}
