using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 冲锋状态
/// </summary>
public class ChargeState : State
{
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    /// <summary>
    /// 状态数据
    /// </summary>
    protected D_ChargeState stateData;

    /// <summary>
    /// 玩家是否在最小仇恨范围内
    /// </summary>
    protected bool isPlayerInMinAggroRange;
    /// <summary>
    /// 是否检测到高角\地面
    /// </summary>
    protected bool isDectectingLedge;
    /// <summary>
    /// 是否检测到墙壁
    /// </summary>
    protected bool isDectectingWall;
    /// <summary>
    /// 冲锋时间是否结束
    /// </summary>
    protected bool isChargeTimeOver;
    /// <summary>
    /// 执行近程动作
    /// </summary>
    protected bool performCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isDectectingLedge = CollisionSenses.LedgeVertical;
        isDectectingWall = CollisionSenses.WallFront;

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
