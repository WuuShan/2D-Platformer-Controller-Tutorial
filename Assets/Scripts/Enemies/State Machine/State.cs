using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态
/// </summary>
public class State
{
    /// <summary>
    /// 状态机
    /// </summary>
    protected FiniteStateMachine stateMachine;
    /// <summary>
    /// 实体
    /// </summary>
    protected Entity entity;
    /// <summary>
    /// 核心
    /// </summary>
    protected Core core;

    /// <summary>
    /// 开始时间
    /// </summary>
    public float startTime { get; protected set; }

    /// <summary>
    /// 动画布尔名称
    /// </summary>
    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.Core;
    }

    /// <summary>
    /// 进入
    /// </summary>
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    /// <summary>
    /// 退出
    /// </summary>
    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public virtual void LogicUpdate()
    {

    }

    /// <summary>
    /// 物理更新
    /// </summary>
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    /// <summary>
    /// 做检查
    /// </summary>
    public virtual void DoChecks()
    {

    }
}
