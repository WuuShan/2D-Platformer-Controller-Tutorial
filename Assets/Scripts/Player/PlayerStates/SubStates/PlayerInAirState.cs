using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家空中状态
/// </summary>
public class PlayerInAirState : PlayerState
{
    /// <summary>
    /// X轴输入
    /// </summary>
    private int xInput;
    /// <summary>
    /// 是否在地面
    /// </summary>
    private bool isGrounded;
    /// <summary>
    /// 是否接触墙壁
    /// </summary>
    private bool isTouchingWall;
    /// <summary>
    /// 跳跃输入
    /// </summary>
    private bool jumpInput;
    /// <summary>
    /// 跳跃输入停止
    /// </summary>
    private bool jumpInputStop;
    /// <summary>
    /// 土狼时间
    /// </summary>
    private bool coyoteTime;
    /// <summary>
    /// 是否在跳跃
    /// </summary>
    private bool isJumping;
    /// <summary>
    /// 抓取输入
    /// </summary>
    private bool grabInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {

            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
        }
    }

    /// <summary>
    /// 检查跳跃倍数
    /// </summary>
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// 检查土狼时间
    /// </summary>
    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    /// <summary>
    /// 开始土狼时间
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;

    /// <summary>
    /// 设置在跳跃
    /// </summary>
    public void SetIsJumping() => isJumping = true;
}
