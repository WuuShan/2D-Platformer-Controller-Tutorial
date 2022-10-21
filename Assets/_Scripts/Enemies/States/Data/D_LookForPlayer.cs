using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 寻找玩家状态数据
/// </summary>
[CreateAssetMenu(fileName = "newLookForPlayerData", menuName = "Data/State Data/LookForPlayer State")]
public class D_LookForPlayer : ScriptableObject
{
    /// <summary>
    /// 转身次数
    /// </summary>
    public int amountOfTurns = 2;
    /// <summary>
    /// 转身之间的时间
    /// </summary>
    public float timeBetweenTurns = 0.75f;
}
