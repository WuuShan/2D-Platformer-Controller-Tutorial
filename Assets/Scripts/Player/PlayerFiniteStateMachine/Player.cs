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
    /// 玩家数据
    /// </summary>
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components 组件
    public Animator Anim { get; private set; }
    /// <summary>
    /// 输入处理程序
    /// </summary>
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms 检查坐标

    /// <summary>
    /// 地面检查坐标
    /// </summary>
    [SerializeField] private Transform groundCheck;
    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    [SerializeField] private Transform wallCheck;

    #endregion

    #region Other Variables 其他变量
    /// <summary>
    /// 当前速度
    /// </summary>
    public Vector2 CurrentVelocity { get; private set; }
    /// <summary>
    /// 目前面对方向 左-1 右1
    /// </summary>
    public int FacingDirection { get; private set; }

    /// <summary>
    /// 实际移动的位移向量
    /// </summary>
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions Unity 回调函数
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions 设置函数
    /// <summary>
    /// 根据移动速度设置X轴位移
    /// </summary>
    /// <param name="velocity">移动速度</param>
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// 根据跳跃速度设置Y轴位移
    /// </summary>
    /// <param name="velocity">跳跃速度</param>
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions 检查函数

    /// <summary>
    /// 检查是否在地面
    /// </summary>
    /// <returns></returns>
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    /// <summary>
    /// 检查是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    /// <summary>
    /// 根据X轴输入检查是否要翻转
    /// </summary>
    /// <param name="xInput">横轴输入</param>
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Functions 其他函数

    /// <summary>
    /// 动画触发
    /// </summary>
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    /// <summary>
    /// 动画结束触发
    /// </summary>
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
