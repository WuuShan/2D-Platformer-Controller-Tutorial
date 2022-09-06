using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统计数据
/// </summary>
public class Stats : CoreComponent
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    [SerializeField] private float maxHealth;
    /// <summary>
    /// 当前生命值
    /// </summary>
    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    /// <summary>
    /// 根据伤害值减少当前生命值
    /// </summary>
    /// <param name="amount">伤害值</param>
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Health is zero!!");
        }
    }

    /// <summary>
    /// 根据恢复值增加当前生命值
    /// </summary>
    /// <param name="amount">恢复值</param>
    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
