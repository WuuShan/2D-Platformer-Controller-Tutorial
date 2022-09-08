using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家地面状态
/// </summary>
public class PlayerGroundedState : PlayerState
{
    /// <summary>
    /// X轴输入
    /// </summary>
    protected int xInput;
    /// <summary>
    /// Y轴输入
    /// </summary>
    protected int yInput;

    /// <summary>
    /// 是否接触天花板
    /// </summary>
    protected bool isTouchingCeiling;

    /// <summary>
    /// 移动组件
    /// </summary>
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    /// <summary>
    /// 移动组件
    /// </summary>
    private Movement movement;

    /// <summary>
    /// 碰撞检查组件
    /// </summary>
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }
    /// <summary>
    /// 碰撞检查组件
    /// </summary>
    private CollisionSenses collisionSenses;

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
    /// <summary>
    /// 是否接触平台
    /// </summary>
    private bool isTouchingLedge;
    /// <summary>
    /// 冲刺输入
    /// </summary>
    private bool dashInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetAmountOfJumpsLeft();
        player.DashState.ResetCanDash();
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
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if (player.InputHandler.attackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if (player.InputHandler.attackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        else if (jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
