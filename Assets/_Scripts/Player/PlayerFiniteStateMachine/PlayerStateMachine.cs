using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态机
/// </summary>
public class PlayerStateMachine
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public PlayerState CurrentState { get; private set; }

    /// <summary>
    /// 根据初始状态对状态机初始化
    /// </summary>
    /// <param name="startingState">初始状态</param>
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    /// <summary>
    /// 退出当前状态并进入新状态
    /// </summary>
    /// <param name="newState">新状态</param>
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
