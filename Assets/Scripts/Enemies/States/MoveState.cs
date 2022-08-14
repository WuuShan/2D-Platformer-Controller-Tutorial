using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动状态
/// </summary>
public class MoveState : State
{
    /// <summary>
    /// 状态数据
    /// </summary>
    protected D_MoveState stateData;

    /// <summary>
    /// 是否检测到墙壁
    /// </summary>
    protected bool isDetectingWall;
    /// <summary>
    /// 是否检测到高角
    /// </summary>
    protected bool isDetectingLedge;
    /// <summary>
    /// 玩家是否在最小仇恨范围内
    /// </summary>
    protected bool isPlayerInMinAggroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
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
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

}
