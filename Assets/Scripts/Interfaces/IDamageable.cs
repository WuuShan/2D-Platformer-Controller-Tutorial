using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可伤害接口
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// 根据伤害值对其扣除生命值
    /// </summary>
    /// <param name="amount">伤害值</param>
    void Damage(float amount);
}
