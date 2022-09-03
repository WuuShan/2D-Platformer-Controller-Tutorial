using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器数据
/// </summary>
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    /// <summary>
    /// 攻击次数
    /// </summary>
    public int amountOfAttacks { get; protected set; }

    /// <summary>
    /// 攻击时的位移
    /// </summary>
    public float[] movementSpeed { get; protected set; }
}
