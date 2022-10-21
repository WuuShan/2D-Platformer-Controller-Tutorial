using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家空中状态
/// </summary>
public class PlayerInAirState : PlayerState
{
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    // Input
    /// <summary>
    /// X轴输入
    /// </summary>
    private int xInput;
    /// <summary>
    /// 跳跃输入
    /// </summary>
    private bool jumpInput;
    /// <summary>
    /// 跳跃输入停止
    /// </summary>
    private bool jumpInputStop;
    /// <summary>
    /// 抓取输入
    /// </summary>
    private bool grabInput;
    /// <summary>
    /// 冲刺输入
    /// </summary>
    private bool dashInput;

    // Checks
    /// <summary>
    /// 是否在地面
    /// </summary>
    private bool isGrounded;
    /// <summary>
    /// 是否接触墙壁
    /// </summary>
    private bool isTouchingWall;
    /// <summary>
    /// 身后是否接触墙壁
    /// </summary>
    private bool isTouchingWallBack;
    /// <summary>
    /// 上一帧是否接触墙壁
    /// </summary>
    private bool oldIsTouchingWall;
    /// <summary>
    /// 上一帧身后是否接触墙壁
    /// </summary>
    private bool oldIsTouchingWallBack;
    /// <summary>
    /// 土狼时间
    /// </summary>
    private bool coyoteTime;
    /// <summary>
    /// 跳墙土狼时间
    /// </summary>
    private bool wallJumpCoyoteTime;
    /// <summary>
    /// 是否在跳跃
    /// </summary>
    private bool isJumping;
    /// <summary>
    /// 是否接触平台
    /// </summary>
    private bool isTouchingLedge;

    /// <summary>
    /// 开始跳墙土狼时间
    /// </summary>
    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingWallBack = CollisionSenses.WallBack;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (player.InputHandler.attackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.attackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (isGrounded && Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == Movement.FacingDirection && Movement.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            Movement.CheckIfShouldFlip(xInput);
            Movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
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
                Movement.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
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
    /// 检查跳墙土狼时间
    /// </summary>
    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    /// <summary>
    /// 开始土狼时间
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;

    /// <summary>
    /// 开始跳墙土狼时间
    /// </summary>
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    /// <summary>
    /// 停止跳墙土狼时间
    /// </summary>
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    /// <summary>
    /// 设置在跳跃
    /// </summary>
    public void SetIsJumping() => isJumping = true;
}
