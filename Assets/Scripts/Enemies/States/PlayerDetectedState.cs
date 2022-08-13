using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家检测状态
/// </summary>
public class PlayerDetectedState : State
{
    /// <summary>
    /// 状态数据
    /// </summary>
    protected D_PlayerDetected stateData;

    /// <summary>
    /// 玩家是否在最小仇恨范围内
    /// </summary>
    protected bool isPlayerInMinAggroRange;
    /// <summary>
    /// 玩家是否在最大仇恨范围内
    /// </summary>
    protected bool isPlayerInMaxAggroRange;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
    }
}
