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

    }
}
