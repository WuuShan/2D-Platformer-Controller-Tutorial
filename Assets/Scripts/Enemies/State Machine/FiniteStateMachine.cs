using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限状态机
/// </summary>
public class FiniteStateMachine
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public State currentState { get; private set; }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <param name="startingState">初始状态</param>
    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newState">新状态</param>
    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
