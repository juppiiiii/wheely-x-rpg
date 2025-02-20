using System.Collections;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    // 기존 코드 유지
    public enum Type { A, B, C, D };
    public Type enemytype;
    public int maxHealth = 15;
    public int currentHealth = 15;
    public float moveSpeed = 0f;
    public int spawnInterval = 0;
    
    public int attackRange = 0;
    public int attackTime = 0;
    public int attackCooldownTimer = 0;
    public int attackDamage = 1;
    public int Number_of_Monster_Appearances = 0;
    public bool isAttacking = false;
    public bool isRanged = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// 데미지를 받는 메서드
    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;
        Debug.Log($"몬스터가 {damage} 피해를 받음. 남은 체력: {currentHealth}");

        // TODO: 피격 효과 추가 (색상 변경 등)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 몬스터 사망 처리
    /// </summary>
    private void Die()
    {
        Debug.Log("몬스터 처치!");
        // TODO: 경험치 드롭, 아이템 드롭 등의 처리
        Destroy(gameObject);
    }

    /// <summary>
    /// 플레이어 공격
    /// </summary>
    public void AttackPlayer(Player player)
    {
        if (!isAttacking && attackCooldownTimer <= 0)
        {
            isAttacking = true;
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }
            attackCooldownTimer = attackTime;
            StartCoroutine(ResetAttackState());
        }
    }

    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }

    void Update()
    {
        // 공격 쿨다운 감소
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer--;
        }
    }
}
