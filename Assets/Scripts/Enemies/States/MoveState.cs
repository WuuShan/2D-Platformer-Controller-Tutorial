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
    /// 是否在检测墙壁
    /// </summary>
    protected bool isDetectingWall;
    /// <summary>
    /// 是否在检测高角
    /// </summary>
    protected bool isDetectingLedge;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
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

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }
}
