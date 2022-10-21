using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 闪避状态
/// </summary>
public class DodgeState : State
{
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_DodgeState stateData;

    /// <summary>
    /// 执行近距离动作
    /// </summary>
    protected bool performCloseRangeAction;
    /// <summary>
    /// 玩家是否在最大仇恨范围内
    /// </summary>
    protected bool isPlayerInMaxAggroRange;
    /// <summary>
    /// 是否在地面
    /// </summary>
    protected bool isGrounded;
    /// <summary>
    /// 是否闪避结束
    /// </summary>
    protected bool isDodgeOver;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isGrounded = CollisionSenses.Ground;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        Movement.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
