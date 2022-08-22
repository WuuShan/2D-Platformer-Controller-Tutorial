using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 移动输入方向
    /// </summary>
    private float movementInputDirection;
    /// <summary>
    /// 跳跃计时器
    /// </summary>
    private float jumpTimer;
    /// <summary>
    /// 转换计时器
    /// </summary>
    private float turnTimer;
    /// <summary>
    /// 跳墙计时器
    /// </summary>
    private float wallJumpTimer;
    /// <summary>
    /// 冲刺剩余时间
    /// </summary>
    private float dashTimeLeft;
    /// <summary>
    /// 上次生成残像的x坐标
    /// </summary>
    private float lastImageXpos;
    /// <summary>
    /// 上次冲刺时间
    /// </summary>
    private float lastDash = -100;
    /// <summary>
    /// 击退开始时间
    /// </summary>
    private float knockbackStartTime;
    /// <summary>
    /// 击退持续时间
    /// </summary>
    [SerializeField] private float knockbackDuration;

    /// <summary>
    /// 剩余跳跃次数
    /// </summary>
    private int amountOfJumpsLeft;
    /// <summary>
    /// 面对方向
    /// </summary>
    private int facingDirection = 1;
    /// <summary>
    /// 最后跳墙方向
    /// </summary>
    private int lastWallJumpDirection;

    /// <summary>
    /// 是否面向右边
    /// </summary>
    private bool isFacingRight = true;
    /// <summary>
    /// 是否在行走
    /// </summary>
    private bool isWalking;
    /// <summary>
    /// 是否在地面
    /// </summary>
    private bool isGrounded;
    /// <summary>
    /// 是否在接触墙
    /// </summary>
    private bool isTouchingWall;
    /// <summary>
    /// 是否在滑墙
    /// </summary>
    private bool isWallSliding;
    /// <summary>
    /// 能否正常跳跃
    /// </summary>
    private bool canNormalJump;
    /// <summary>
    /// 能否跳墙
    /// </summary>
    private bool canWallJump;
    /// <summary>
    /// 是否在尝试跳跃
    /// </summary>
    private bool isAttemptingToJump;
    /// <summary>
    /// 检查跳跃倍数是否应用
    /// </summary>
    private bool checkJumpMultiplier;
    /// <summary>
    /// 能否移动
    /// </summary>
    private bool canMove;
    /// <summary>
    /// 能否翻转
    /// </summary>
    private bool canFlip;
    /// <summary>
    /// 有无跳墙
    /// </summary>
    private bool hasWallJumped;
    /// <summary>
    /// 是否在接触高角
    /// </summary>
    private bool isTouchingLedge;
    /// <summary>
    /// 能否攀爬高角
    /// </summary>
    private bool canClimbLedge = false;
    /// <summary>
    /// 检测到高角
    /// </summary>
    private bool ledgeDetected;
    /// <summary>
    /// 是否在冲刺
    /// </summary>
    private bool isDashing;
    /// <summary>
    /// 击退
    /// </summary>
    private bool knockback;

    /// <summary>
    /// 击退速度向量
    /// </summary>
    [SerializeField] private Vector2 knockbackSpeed;

    /// <summary>
    /// 高角位置底部
    /// </summary>
    private Vector2 ledgePosBot;
    /// <summary>
    /// 高角原先位置
    /// </summary>
    private Vector2 ledgePos1;
    /// <summary>
    /// 高角终点位置
    /// </summary>
    private Vector2 ledgePos2;

    /// <summary>
    /// 刚体组件
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// 动画机组件
    /// </summary>
    private Animator anim;

    /// <summary>
    /// 跳跃次数
    /// </summary>
    public int amountOfJumps = 1;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float movementSpeed = 10.0f;
    /// <summary>
    /// 跳跃力
    /// </summary>
    public float jumpForce = 16.0f;
    /// <summary>
    /// 检查地面范围
    /// </summary>
    public float groundCheckRadius;
    /// <summary>
    /// 检查墙壁距离
    /// </summary>
    public float wallCheckDistance;
    /// <summary>
    /// 滑墙速度
    /// </summary>
    public float wallSlideSpeed;
    /// <summary>
    /// 空中移动力
    /// </summary>
    public float movementForceInAir;
    /// <summary>
    /// 空气阻力倍数
    /// </summary>
    public float airDragMultiplier = 0.95f;
    /// <summary>
    /// 可变跳跃高度倍数
    /// </summary>
    public float variableJumpHeightMultiplier = 0.5f;
    /// <summary>
    /// 蹬墙力
    /// </summary>
    public float wallHopForce;
    /// <summary>
    /// 跳墙力
    /// </summary>
    public float wallJumpForce;
    /// <summary>
    /// 跳跃计时器设置
    /// </summary>
    public float jumpTimerSet = 0.15f;
    /// <summary>
    /// 转换计时器设置
    /// </summary>
    public float turnTimerSet = 0.1f;
    /// <summary>
    /// 跳墙计时器设置
    /// </summary>
    public float wallJumpTimerSet = 0.5f;
    // 高角攀爬的位置偏移量
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    /// <summary>
    /// 冲刺时间
    /// </summary>
    public float dashTime;
    /// <summary>
    /// 冲刺速度
    /// </summary>
    public float dashSpeed;
    /// <summary>
    /// 残影间距
    /// </summary>
    public float distanceBetweenImages;
    /// <summary>
    /// 冲刺冷却
    /// </summary>
    public float dashCoolDown;

    /// <summary>
    /// 蹬墙方向
    /// </summary>
    public Vector2 wallHopDirection;
    /// <summary>
    /// 跳墙方向
    /// </summary>
    public Vector2 wallJumpDirection;

    /// <summary>
    /// 地面检查坐标
    /// </summary>
    public Transform groundCheck;
    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    public Transform wallCheck;
    /// <summary>
    /// 高角检查坐标
    /// </summary>
    public Transform ledgeCheck;

    /// <summary>
    /// 地面图层蒙版
    /// </summary>
    public LayerMask whatIsGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
        CheckKnockback();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    /// <summary>
    /// 检查是否可以滑墙
    /// </summary>
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    /// <summary>
    /// 获取冲刺状态
    /// </summary>
    /// <returns></returns>
    public bool GetDashStatus()
    {
        return isDashing;
    }

    /// <summary>
    /// 根据方向击退到指定距离
    /// </summary>
    /// <param name="direction"></param>
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    /// <summary>
    /// 检查击退
    /// </summary>
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    /// <summary>
    /// 检查高角攀爬
    /// </summary>
    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    /// <summary>
    /// 完成高角攀爬
    /// </summary>
    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }

    /// <summary>
    /// 检查环境
    /// </summary>
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    /// <summary>
    /// 检查是否可以跳跃
    /// </summary>
    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    /// <summary>
    /// 检查移动方向
    /// </summary>
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    /// <summary>
    /// 更新动画
    /// </summary>
    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    /// <summary>
    /// 检查输入
    /// </summary>
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");    // A -1 D 1

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    /// <summary>
    /// 尝试冲刺
    /// </summary>
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    /// <summary>
    /// 获取角色面向方向
    /// </summary>
    /// <returns>左-1 右1</returns>
    public int GetFacingDirection()
    {
        return facingDirection;
    }

    /// <summary>
    /// 检查冲刺
    /// </summary>
    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall || isDashing == false)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }
    }

    /// <summary>
    /// 检查跳跃
    /// </summary>
    private void CheckJump()
    {
        if (jumpTimer > 0)
        {
            // 跳墙
            if (!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (isGrounded)
            {
                NormalJump();
            }
        }

        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0)
        {
            if (hasWallJumped && movementInputDirection == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 正常跳跃
    /// </summary>
    private void NormalJump()
    {
        if (canNormalJump)  // 地面跳跃
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    /// <summary>
    /// 墙壁跳跃
    /// </summary>
    private void WallJump()
    {
        if (canWallJump)   // 在滑墙或者接触墙时 控制方向跳跃
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    /// <summary>
    /// 应用移动
    /// </summary>
    private void ApplyMovement()
    {
        if (!isGrounded && !isWallSliding && movementInputDirection == 0 && !knockback)  // 在空中不移动
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !knockback)    // 在地面移动
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isWallSliding)  // 在墙上滑动
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    /// <summary>
    /// 禁用翻转
    /// </summary>
    public void DisableFlip()
    {
        canFlip = false;
    }

    /// <summary>
    /// 启用翻转
    /// </summary>
    public void EnableFlip()
    {
        canFlip = true;
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        if (!isWallSliding && canFlip && !knockback)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
