using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗相关
/// </summary>
public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    /// <summary>
    /// 是否激活击退
    /// </summary>
    private bool isKnockbackActive;
    /// <summary>
    /// 击退开始时间
    /// </summary>
    private float knockbackStartTime;

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;

        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.Ground)
        {
            isKnockbackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }
}
