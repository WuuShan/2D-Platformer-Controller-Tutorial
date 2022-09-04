using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 击退接口
/// </summary>
public interface IKnockbackable
{
    /// <summary>
    /// 往一个方向击退
    /// </summary>
    /// <param name="angle">角度</param>
    /// <param name="strength">力度</param>
    /// <param name="direction">方向</param>
    void Knockback(Vector2 angle, float strength, int direction);
}
