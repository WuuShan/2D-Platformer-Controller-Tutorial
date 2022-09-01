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
    public PlayerInventory Inventory { get; private set; }

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
    /// <summary>
    /// 平台检查坐标
    /// </summary>
    [SerializeField] private Transform ledgeCheck;
    /// <summary>
    /// 天花板检查坐标
    /// </summary>
    [SerializeField] private Transform CeilingCheck;

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
        Inventory = GetComponent<PlayerInventory>();

        FacingDirection = 1;

        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);

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
    /// 设置速度为零
    /// </summary>
    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    /// <summary>
    /// 设置矢量
    /// </summary>
    /// <param name="velocity">速度</param>
    /// <param name="angle">角度</param>
    /// <param name="direction">方向</param>
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// 根据向量和速度设置矢量
    /// </summary>
    /// <param name="velocity">速度</param>
    /// <param name="direction">向量</param>
    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// 根据速度设置X轴移动
    /// </summary>
    /// <param name="velocity">速度</param>
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    /// <summary>
    /// 根据速度设置Y轴移动
    /// </summary>
    /// <param name="velocity">速度</param>
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions 检查函数

    /// <summary>
    /// 检查天花板
    /// </summary>
    /// <returns></returns>
    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(CeilingCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

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
    /// 检查是否接触平台
    /// </summary>
    /// <returns></returns>
    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    /// <summary>
    /// 检查身后是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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

    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        workspace.Set(MovementCollider.size.x, height);

        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.size = workspace;
        MovementCollider.offset = center;
    }

    /// <summary>
    /// 确定玩家平台攀爬后到达的内角位置
    /// </summary>
    /// <returns>内角位置</returns>
    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;
        workspace.Set((xDist + 0.015f) * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);
        return workspace;
    }

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
