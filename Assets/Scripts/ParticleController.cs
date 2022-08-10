using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 粒子控制器
/// </summary>
public class ParticleController : MonoBehaviour
{
    /// <summary>
    /// 完成动画
    /// </summary>
    private void FinishAnim()
    {
        Destroy(gameObject);
    }
}
