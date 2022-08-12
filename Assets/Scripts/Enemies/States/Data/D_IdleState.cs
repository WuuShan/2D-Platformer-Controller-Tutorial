using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 待机状态数据
/// </summary>
[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    /// <summary>
    /// 最小待机时间
    /// </summary>
    public float minIdleTime = 1f;
    /// <summary>
    /// 最大待机时间
    /// </summary>
    public float maxIdleTime = 2f;
}
