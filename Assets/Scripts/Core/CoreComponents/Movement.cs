using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理刚体移动
/// </summary>
public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

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

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion

}
