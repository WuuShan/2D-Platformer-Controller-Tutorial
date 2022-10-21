using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 眩晕状态数据
/// </summary>
[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    /// <summary>
    /// 眩晕时间
    /// </summary>
    public float stunTime = 3f;

    /// <summary>
    /// 眩晕击退时间
    /// </summary>
    public float stunKnockbackTime = 0.2f;
    /// <summary>
    /// 眩晕击退位移
    /// </summary>
    public float stunKnockbackSpeed = 20f;
    /// <summary>
    /// 眩晕击退角度
    /// </summary>
    public Vector2 stunKnockbackAngle;
}
