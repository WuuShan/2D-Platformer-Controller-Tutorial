using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机的动画
/// </summary>
public class AnimationToStatemachine : MonoBehaviour
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public AttackState attackState;

    /// <summary>
    /// 触发攻击
    /// </summary>
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    /// <summary>
    /// 完成攻击
    /// </summary>
    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
}
