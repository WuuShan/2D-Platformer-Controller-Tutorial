using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家接触墙状态
/// </summary>
public class PlayerToucingWallState : PlayerState
{
    /// <summary>
    /// 是否在地面
    /// </summary>
    protected bool isGrounded;
    /// <summary>
    /// 是否接触墙壁
    /// </summary>
    protected bool isTouchingWall;
    /// <summary>
    /// 抓取输入
    /// </summary>
    protected bool grabInput;
    /// <summary>
    /// X轴输入
    /// </summary>
    protected int xInput;
    /// <summary>
    /// Y轴输入
    /// </summary>
    protected int yInput;

    public PlayerToucingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        grabInput = player.InputHandler.GrabInput;

        if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || (xInput != player.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
