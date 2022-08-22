using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    /// <summary>
    /// 移动速度
    /// </summary>
    [Header("Move State")]
    public float movementVelocity = 10f;
}
