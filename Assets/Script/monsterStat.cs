using System.Collections;
using UnityEngine;

public class monsterStat : MonoBehaviour
{
    // ���� Ÿ�� 
    public enum Type { A, B, C, D };
    public Type enemytype;
    // ���� ����
    public int maxHealth = 15;
    public int currentHealth = 15;
    public float moveSpeed = 0f;
    public int spawnInterval = 0;
    
    public int attackRange = 0;
    public int attackTime = 0;
    public int attackCooldownTimer = 0; // ���� ��Ÿ�� �ǽð� �ݿ�
    public int attackDamage = 1;        // ���� ������Ʈ�� �����ϴ� ������
    public int Number_of_Monster_Appearances = 0;
    public bool isAttacking = false;
    public bool isRanged = false;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shotDamage();
        //ChangeColorCoroutine();
    }

    void monsterAttack()
    {

    }

    void takeDamage(int Damage)
    {
        // �������� �޴� ����
    }

    //void shotDamage()
    //{
    //    if (attackCooldown >= 0)
    //    {
    //        // ���� ����
    //        //Debug.Log("������ �����߽��ϴ�! ������: " + attackDamage);
    //        // ��Ÿ�� ����
    //        attackCooldown -= 1;
    //        attackCooldownTimer = attackCooldown;
    //    }
       
        
    //    Debug.Log("��Ÿ�� ���Դϴ�. ���� �ð�: " + attackCooldownTimer);
    //}

    

    
}
