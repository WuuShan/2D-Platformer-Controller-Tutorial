using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    /// <summary>
    /// 移动速度
    /// </summary>
    [Header("Move State")]
    public float movementVelocity = 10f;

    /// <summary>
    /// 跳跃速度
    /// </summary>
    [Header("Jump State")]
    public float jumpVelocity = 15f;
    /// <summary>
    /// 跳跃次数
    /// </summary>
    public int amountOfJumps = 1;

    /// <summary>
    /// 跳墙速度
    /// </summary>
    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    /// <summary>
    /// 跳墙时间
    /// </summary>
    public float wallJumpTime = 0.4f;
    /// <summary>
    /// 跳墙角度
    /// </summary>
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    /// <summary>
    /// 土狼时间
    /// </summary>
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    /// <summary>
    /// 可变跳跃高度乘数
    /// </summary>
    public float variableJumpHeightMultiplier = 0.5f;

    /// <summary>
    /// 滑墙速度
    /// </summary>
    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    /// <summary>
    /// 爬墙速度
    /// </summary>
    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    /// <summary>
    /// 起始偏移量
    /// </summary>
    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    /// <summary>
    /// 停下偏移量
    /// </summary>
    public Vector2 stopOffset;

    /// <summary>
    /// 冲刺冷却
    /// </summary>
    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    /// <summary>
    /// 时停极限
    /// </summary>
    public float maxHoldTime = 1f;
    /// <summary>
    /// 时间放慢倍速
    /// </summary>
    public float holdTimeScale = 0.25f;
    /// <summary>
    /// 冲刺时间
    /// </summary>
    public float dashTime = 0.2f;
    /// <summary>
    /// 冲刺速度
    /// </summary>
    public float dashVelocity = 30f;
    /// <summary>
    /// 空气阻力
    /// </summary>
    public float drag = 10f;
    /// <summary>
    /// 冲刺结束Y乘数
    /// </summary>
    public float dashEndYMultiplier = 0.2f;
    /// <summary>
    /// 残影之间的距离
    /// </summary>
    public float distBetweenAfterImages = 0.5f;

    /// <summary>
    /// 蹲伏移动速度
    /// </summary>
    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    /// <summary>
    /// 蹲伏碰撞器高度
    /// </summary>
    public float crouchColliderHeight = 0.8f;
    /// <summary>
    /// 站立碰撞器高度
    /// </summary>
    public float standColliderHeight = 1.6f;
}
