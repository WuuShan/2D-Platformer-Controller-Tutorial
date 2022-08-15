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
    /// <summary>
    /// 执行远程动作
    /// </summary>
    protected bool performLongRangeAction;
    /// <summary>
    /// 执行近程动作
    /// </summary>
    protected bool performCloseRangeAction;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
