using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家输入处理程序
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    /// <summary>
    /// 原始移动输入
    /// </summary>
    public Vector2 RawMovementInput { get; private set; }
    /// <summary>
    /// 原始冲刺方向输入
    /// </summary>
    public Vector2 RawDashDirectionInput { get; private set; }
    /// <summary>
    /// 冲刺方向输入
    /// </summary>
    public Vector2Int DashDirectionInput { get; private set; }
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
    /// 抓取输入
    /// </summary>
    public bool GrabInput { get; private set; }
    /// <summary>
    /// 冲刺输入
    /// </summary>
    public bool DashInput { get; private set; }
    /// <summary>
    /// 冲刺输入停止
    /// </summary>
    public bool DashInputStop { get; private set; }

    /// <summary>
    /// 预输入时间
    /// </summary>
    [SerializeField] private float inputHoldTime = 0.2f;

    /// <summary>
    /// 跳跃输入开始时间
    /// </summary>
    private float jumpInputStartTime;
    /// <summary>
    /// 冲刺输入开始时间
    /// </summary>
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    /// <summary>
    /// 注册移动输入
    /// </summary>
    /// <param name="context"></param>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }


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
    /// 注册抓取输入
    /// </summary>
    /// <param name="context"></param>
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    /// <summary>
    /// 注册冲刺输入
    /// </summary>
    /// <param name="context"></param>
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    /// <summary>
    /// 注册冲刺方向输入
    /// </summary>
    /// <param name="context"></param>
    public void OnDashIDirectionnput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    /// <summary>
    /// 使用跳跃输入
    /// </summary>
    public void UseJumpInput() => JumpInput = false;

    /// <summary>
    /// 使用冲刺输入
    /// </summary>
    public void UseDashInput() => DashInput = false;

    /// <summary>
    /// 检查跳跃预输入时间
    /// </summary>
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    /// <summary>
    /// 检查冲刺预输入时间
    /// </summary>
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}
