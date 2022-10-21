using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 远程攻击状态数据
/// </summary>
[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    /// <summary>
    /// 发射物
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// 发射物伤害
    /// </summary>
    public float projectileDamage = 10f;
    /// <summary>
    /// 发射物速度
    /// </summary>
    public float projectileSpeed = 12f;
    /// <summary>
    /// 发射物行进距离
    /// </summary>
    public float projectileTravelDistance;
}
