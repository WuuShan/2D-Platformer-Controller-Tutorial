using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
public class PlayerStats : MonoBehaviour
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    [SerializeField] private float maxHealth;

    /// <summary>
    /// 死亡粒子特效
    /// </summary>
    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    /// <summary>
    /// 当前生命值
    /// </summary>
    private float currentHealth;

    private GameManager GM;

    private void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// 根据伤害降低生命值
    /// </summary>
    /// <param name="amount">伤害</param>
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
