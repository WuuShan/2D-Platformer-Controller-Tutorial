using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家检测数据
/// </summary>
[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/PlayerDetected State")]
public class D_PlayerDetected : ScriptableObject
{
    /// <summary>
    /// 远程动作时间
    /// </summary>
    public float longRangeActionTime = 1.5f;
}
