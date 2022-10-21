using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 闪避状态数据
/// </summary>
[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge State")]
public class D_DodgeState : ScriptableObject
{
    /// <summary>
    /// 闪避速度
    /// </summary>
    public float dodgeSpeed = 10f;
    /// <summary>
    /// 闪避时间
    /// </summary>
    public float dodgeTime = 0.2f;
    /// <summary>
    /// 闪避冷却
    /// </summary>
    public float dodgeCooldown = 2f;
    /// <summary>
    /// 闪避角度
    /// </summary>
    public Vector2 dodgeAngle;
}
