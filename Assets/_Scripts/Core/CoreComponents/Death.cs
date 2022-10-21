using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体的生命值达到零时，调用
/// </summary>
public class Death : CoreComponent
{
    private ParticleManager ParticleManager { get => particleManager ??= core.GetCoreComponent<ParticleManager>(); }
    private ParticleManager particleManager;
    private Stats Stats { get => stats ??= core.GetCoreComponent<Stats>(); }
    private Stats stats;

    public override void Init(Core core)
    {
        base.Init(core);

        Stats.HealthZero += Die;
    }

    private void OnEnable()
    {
        // Stats.HealthZero += Die;
    }

    private void OnDisable()
    {
        Stats.HealthZero -= Die;
    }

    /// <summary>
    /// 死亡粒子
    /// </summary>
    [SerializeField] private GameObject[] deathParticles;

    /// <summary>
    /// 死亡
    /// </summary>
    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            ParticleManager.StartParticles(particle);
        }

        core.transform.parent.gameObject.SetActive(false);
    }
}
