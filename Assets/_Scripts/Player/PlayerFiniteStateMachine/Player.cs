using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家
/// </summary>
public class Player : MonoBehaviour
{
    #region State Variables 状态变量
    /// <summary>
    /// 状态机
    /// </summary>
    public PlayerStateMachine StateMachine { get; private set; }

    /// <summary>
    /// 待机状态
    /// </summary>
    public PlayerIdleState IdleState { get; private set; }
    /// <summary>
    /// 移动状态
    /// </summary>
    public PlayerMoveState MoveState { get; private set; }
    /// <summary>
    /// 跳跃状态
    /// </summary>
    public PlayerJumpState JumpState { get; private set; }
    /// <summary>
    /// 空中状态
    /// </summary>
    public PlayerInAirState InAirState { get; private set; }
    /// <summary>
    /// 落地状态
    /// </summary>
    public PlayerLandState LandState { get; private set; }
    /// <summary>
    /// 滑墙状态
    /// </summary>
    public PlayerWallSlideState WallSlideState { get; private set; }
    /// <summary>
    /// 抓墙状态
    /// </summary>
    public PlayerWallGrabState WallGrabState { get; private set; }
    /// <summary>
    /// 爬墙状态
    /// </summary>
    public PlayerWallClimbState WallClimbState { get; private set; }
    /// <summary>
    /// 跳墙状态
    /// </summary>
    public PlayerWallJumpState WallJumpState { get; private set; }
    /// <summary>
    /// 平台攀爬状态
    /// </summary>
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    /// <summary>
    /// 冲刺状态
    /// </summary>
    public PlayerDashState DashState { get; private set; }
    /// <summary>
    /// 蹲伏待机状态
    /// </summary>
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    /// <summary>
    /// 蹲伏移动状态
    /// </summary>
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }
    /// <summary>
    /// 主要攻击状态
    /// </summary>
    public PlayerAttackState PrimaryAttackState { get; private set; }
    /// <summary>
    /// 次要攻击状态
    /// </summary>
    public PlayerAttackState SecondaryAttackState { get; private set; }


    /// <summary>
    /// 玩家数据
    /// </summary>
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components 组件

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    /// <summary>
    /// 输入处理程序
    /// </summary>
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    /// <summary>
    /// 冲刺方向指示器坐标
    /// </summary>
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    #endregion

    #region Other Variables 其他变量

    /// <summary>
    /// 实际移动的位移向量
    /// </summary>
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions Unity 回调函数
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        MovementCollider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions 其他函数

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    /// <summary>
    /// 动画触发
    /// </summary>
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    /// <summary>
    /// 动画结束触发
    /// </summary>
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    #endregion
}
