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
    /// 墙壁检查距离
    /// </summary>
    public float wallCheckDistance = 0.2f;
    /// <summary>
    /// 高角检查距离
    /// </summary>
    public float ledgeCheckDistance = 0.4f;

    /// <summary>
    /// 最小仇恨距离
    /// </summary>
    public float minAggroDistance = 3f;
    /// <summary>
    /// 最大仇恨距离
    /// </summary>
    public float maxAggroDistance = 4f;

    /// <summary>
    /// 地面图层蒙版
    /// </summary>
    public LayerMask whatIsGround;
    /// <summary>
    /// 玩家图层蒙版
    /// </summary>
    public LayerMask whatIsPlayer;
}
