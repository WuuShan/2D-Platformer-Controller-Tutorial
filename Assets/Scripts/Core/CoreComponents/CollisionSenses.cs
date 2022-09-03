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
    public Transform GroundCheck
    {
        get
        {
            if (groundCheck)
            {
                return groundCheck;
            }

            Debug.LogError("No Ground Check on " + core.transform.parent.name);
            return null;
        }

        private set => groundCheck = value;
    }
    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    public Transform WallCheck
    {
        get
        {
            if (wallCheck)
            {
                return wallCheck;
            }

            Debug.LogError("No wall Check on " + core.transform.parent.name);
            return null;
        }

        private set => wallCheck = value;
    }
    /// <summary>
    /// 平台横向检查坐标
    /// </summary>
    public Transform LedgeCheckHorizontal
    {
        get
        {
            if (ledgeCheckHorizontal)
            {
                return ledgeCheckHorizontal;
            }

            Debug.LogError("No ledge Check Horizontal on " + core.transform.parent.name);
            return null;
        }

        private set => ledgeCheckHorizontal = value;
    }
    /// <summary>
    /// 平台纵向检查坐标
    /// </summary>
    public Transform LedgeCheckVertical
    {
        get
        {
            if (ledgeCheckVertical)
            {
                return ledgeCheckVertical;
            }

            Debug.LogError("No ledge Check Vertical on " + core.transform.parent.name);
            return null;
        }
        private set => ledgeCheckVertical = value;
    }
    /// <summary>
    /// 天花板检查坐标
    /// </summary>
    public Transform CeilingCheck
    {
        get
        {
            if (ceilingCheck)
            {
                return ceilingCheck;
            }

            Debug.LogError("No ceiling Check on " + core.transform.parent.name);
            return null;
        }
        private set => ceilingCheck = value;
    }

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
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
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
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// 检查是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 横向检查是否接触平台
    /// </summary>
    /// <returns></returns>
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 纵向检查是否接触平台
    /// </summary>
    /// <returns></returns>
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 检查身后是否接触墙壁
    /// </summary>
    /// <returns></returns>
    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    #endregion
}
