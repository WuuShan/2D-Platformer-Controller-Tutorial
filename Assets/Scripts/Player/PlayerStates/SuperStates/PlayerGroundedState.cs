using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家地面状态
/// </summary>
public class PlayerGroundedState : PlayerState
{
    /// <summary>
    /// 横轴输入
    /// </summary>
    protected int xInput;

    /// <summary>
    /// 跳跃输入
    /// </summary>
    private bool jumpInput;
    /// <summary>
    /// 抓取输入
    /// </summary>
    private bool grabInput;
    /// <summary>
    /// 是否在地面
    /// </summary>
    private bool isGrounded;
    /// <summary>
    /// 是否接触墙壁
    /// </summary>
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

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

        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

        if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
