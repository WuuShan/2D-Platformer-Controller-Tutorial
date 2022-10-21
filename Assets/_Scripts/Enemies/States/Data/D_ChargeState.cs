using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冲锋状态数据
/// </summary>
[CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
public class D_ChargeState : ScriptableObject
{
    /// <summary>
    /// 冲锋时间
    /// </summary>
    public float chargeSpeed = 6f;
    /// <summary>
    /// 冲锋时间
    /// </summary>
    public float chargeTime = 2f;
}
