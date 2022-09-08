using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗相关
/// </summary>
public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }
    private Stats Stats { get => stats ??= core.GetCoreComponent<Stats>(); }
    private ParticleManager ParticleManager { get => particleManager ??= core.GetCoreComponent<ParticleManager>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    private ParticleManager particleManager;

    /// <summary>
    /// 最大击退时间
    /// </summary>
    [SerializeField] private float maxKnockbackTime = 0.2f;

    /// <summary>
    /// 伤害粒子
    /// </summary>
    [SerializeField] private GameObject damageParticles;

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
    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        Stats?.DecreaseHealth(amount);
        ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;

        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
