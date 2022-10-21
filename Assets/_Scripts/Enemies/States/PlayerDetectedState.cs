using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家检测状态
/// </summary>
public class PlayerDetectedState : State
{
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

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
    /// <summary>
    /// 是否检测到高角
    /// </summary>
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(0f);

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
