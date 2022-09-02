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
    /// 移动速度
    /// </summary>
    public float[] movementSpeed;
}
