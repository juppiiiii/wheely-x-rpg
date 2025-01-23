using System.Collections;
using UnityEngine;

public class monsterStat : MonoBehaviour
{
    // 몬스터 타입 
    public enum Type { A, B, C, D };
    public Type enemytype;
    // 몬스터 스탯
    public int maxHealth = 15;
    public int currentHealth = 15;
    public float moveSpeed = 0f;
    public int spawnInterval = 0;
    
    public int attackRange = 0;
    public int attackTime = 0;
    public int attackCooldownTimer = 0; // 남은 쿨타임 실시간 반영
    public int attackDamage = 1;        // 몬스터 오브젝트가 공격하는 데미지
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
        // 데미지를 받는 로직
    }

    //void shotDamage()
    //{
    //    if (attackCooldown >= 0)
    //    {
    //        // 공격 수행
    //        //Debug.Log("공격을 수행했습니다! 데미지: " + attackDamage);
    //        // 쿨타임 설정
    //        attackCooldown -= 1;
    //        attackCooldownTimer = attackCooldown;
    //    }
       
        
    //    Debug.Log("쿨타임 중입니다. 남은 시간: " + attackCooldownTimer);
    //}

    

    
}
