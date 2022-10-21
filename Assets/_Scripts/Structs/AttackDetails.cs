using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// 击退力度
    /// </summary>
    public float knockbackStrenght;
    /// <summary>
    /// 击退角度
    /// </summary>
    public Vector2 knockbackAngle;
}