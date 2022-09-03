using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理碰撞检查
/// </summary>
public class CollisionSenses : CoreComponent
{

    #region Check Transforms 检查坐标

    /// <summary>
    /// 地面检查坐标
    /// </summary>
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    /// <summary>
    /// 平台检查坐标
    /// </summary>
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
    /// <summary>
    /// 天花板检查坐标
    /// </summary>
    public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }

    /// <summary>
    /// 地面检查范围
    /// </summary>
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    /// <summary>
    /// 墙壁检查距离
    /// </summary>
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    /// <summary>
    /// 地面图层
    /// </summary>
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ceilingCheck;


    [SerializeField] private float groundCheckRadius;

    [SerializeField] private float wallCheckDistance;


    [SerializeField] private LayerMask whatIsGround;

    #endregion

    #region Check Functions 检查函数

    /// <summary>
    /// 检查天花板
    /// </summary>
    /// <returns></returns>
    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// 检查是否在地面
    /// </summary>
    /// <returns></returns>
    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// 检查是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool WallFront
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 检查是否接触平台
    /// </summary>
    /// <returns></returns>
    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 检查身后是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    #endregion
}
