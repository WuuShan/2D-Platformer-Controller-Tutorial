using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击详情
/// </summary>
public struct AttackDetails
{
    /// <summary>
    /// 坐标
    /// </summary>
    public Vector2 position;
    /// <summary>
    /// 伤害值
    /// </summary>
    public float damageAmount;
    /// <summary>
    /// 减少攻击对象对应的眩晕抗性
    /// </summary>
    public float stunDamageAmount;
}

/// <summary>
/// 武器攻击详情
/// </summary>
[System.Serializable]
public struct WeaponAttackDetails
{
    /// <summary>
    /// 攻击名称
    /// </summary>
    public string attackName;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float movementSpeed;
    /// <summary>
    /// 伤害值
    /// </summary>
    public float damageAmount;
}