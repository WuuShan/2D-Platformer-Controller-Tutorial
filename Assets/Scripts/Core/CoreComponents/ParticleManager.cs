using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 特效粒子管理器
/// </summary>
public class ParticleManager : CoreComponent
{
    /// <summary>
    /// <para>Transform that will be the parent of spawned particles</para>
    /// <para>将作为生成粒子的父级 Transform</para>
    /// </summary>
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();

        // Setting the reference
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    /// <summary>
    /// 实例化随机旋转的粒子
    /// </summary>
    /// <param name="particlesPrefab">粒子预制件</param>
    /// <returns></returns>
    public GameObject StartParticlesWithRandomRotation(GameObject particlesPrefab)
    {
        // Generate a random rotation along the z-axis 沿 z 轴生成随机旋转
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        // Spawn the particle and return 生成粒子并返回
        return StartParticles(particlesPrefab, transform.position, randomRotation);
    }

    /// <summary>
    /// 实例化粒子
    /// </summary>
    /// <param name="particlesPrefab">粒子预制件</param>
    /// <returns></returns>
    public GameObject StartParticles(GameObject particlesPrefab)
    {
        return StartParticles(particlesPrefab, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 实例化指定位置和旋转的粒子
    /// </summary>
    /// <param name="particlesPrefab">粒子预制件</param>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    /// <returns></returns>
    public GameObject StartParticles(GameObject particlesPrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlesPrefab, position, rotation, particleContainer);
    }
}
