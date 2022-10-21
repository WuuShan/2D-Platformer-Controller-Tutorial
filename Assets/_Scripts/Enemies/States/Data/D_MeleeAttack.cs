using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近战攻击数据
/// </summary>
[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttack : ScriptableObject
{
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float attackRadius = 0.5f;
    /// <summary>
    /// 攻击伤害
    /// </summary>
    public float attackDamage = 10f;

    /// <summary>
    /// 击退角度
    /// </summary>
    public Vector2 knockbackAngle = Vector2.one;
    /// <summary>
    /// 击退力度
    /// </summary>
    public float knockbackStrength = 10f;

    /// <summary>
    /// 玩家图层蒙版
    /// </summary>
    public LayerMask whatIsPlayer;
}
