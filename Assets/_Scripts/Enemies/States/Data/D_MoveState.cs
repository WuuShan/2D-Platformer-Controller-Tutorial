using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动状态数据
/// </summary>
[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float movementSpeed = 3f;
}
