using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家输入处理程序
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// 原始移动输入
    /// </summary>
    public Vector2 RawMovementInput { get; private set; }
    /// <summary>
    /// 标准化输入X
    /// </summary>
    public int NormInputX { get; private set; }
    /// <summary>
    /// 标准化输入Y
    /// </summary>
    public int NormInputY { get; private set; }
    /// <summary>
    /// 跳跃输入
    /// </summary>
    public bool JumpInput { get; private set; }
    /// <summary>
    /// 跳跃输入停止
    /// </summary>
    public bool JumpInputStop { get; private set; }

    /// <summary>
    /// 输入保持时间
    /// </summary>
    [SerializeField] private float inputHoldTime = 0.2f;

    /// <summary>
    /// 跳跃输入开始时间
    /// </summary>
    private float jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    /// <summary>
    /// 注册移动输入
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    /// <summary>
    /// 注册跳跃输入
    /// </summary>
    /// <param name="context"></param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)    // 按下
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)   // 松开
        {
            JumpInputStop = true;
        }
    }

    /// <summary>
    /// 使用跳跃输入
    /// </summary>
    public void UseJumpInput() => JumpInput = false;

    /// <summary>
    /// 检查跳跃输入保存时间
    /// </summary>
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
