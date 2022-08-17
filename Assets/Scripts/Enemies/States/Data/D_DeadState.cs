using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 死亡状态数据
/// </summary>
[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    /// <summary>
    /// 死亡躯体粒子
    /// </summary>
    public GameObject deathChunkParticle;
    /// <summary>
    /// 死亡血液粒子
    /// </summary>
    public GameObject deathBloodParticle;
}
