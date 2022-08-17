using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体数据
/// </summary>
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    public float maxHealth = 30f;

    /// <summary>
    /// 击飞高度
    /// </summary>
    public float damageHopSpeed = 3f;

    /// <summary>
    /// 墙壁检查距离
    /// </summary>
    public float wallCheckDistance = 0.2f;
    /// <summary>
    /// 高角检查距离
    /// </summary>
    public float ledgeCheckDistance = 0.4f;
    /// <summary>
    /// 地面检查范围
    /// </summary>
    public float groundCheckRadius = 0.3f;

    /// <summary>
    /// 最小仇恨距离
    /// </summary>
    public float minAggroDistance = 3f;
    /// <summary>
    /// 最大仇恨距离
    /// </summary>
    public float maxAggroDistance = 4f;

    /// <summary>
    /// 眩晕抗性
    /// </summary>
    public float stunResistance = 3f;
    /// <summary>
    /// 未被攻击时，眩晕恢复时间
    /// </summary>
    public float stunRecoveryTime = 2f;

    /// <summary>
    /// 近程动作距离
    /// </summary>
    public float closeRangeActionDistance = 1f;

    /// <summary>
    /// 命中粒子
    /// </summary>
    public GameObject hitParticle;

    /// <summary>
    /// 地面图层蒙版
    /// </summary>
    public LayerMask whatIsGround;
    /// <summary>
    /// 玩家图层蒙版
    /// </summary>
    public LayerMask whatIsPlayer;
}
