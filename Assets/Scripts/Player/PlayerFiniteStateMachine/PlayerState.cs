using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态
/// </summary>
public class PlayerState
{
    /// <summary>
    /// 玩家
    /// </summary>
    protected Player player;
    /// <summary>
    /// 状态机
    /// </summary>
    protected PlayerStateMachine stateMachine;
    /// <summary>
    /// 玩家数据
    /// </summary>
    protected PlayerData playerData;

    /// <summary>
    /// 动画是否结束
    /// </summary>
    protected bool isAnimationFinished;

    /// <summary>
    /// 开始时间
    /// </summary>
    protected float startTime;

    /// <summary>
    /// 动画布尔名称
    /// </summary>
    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    /// <summary>
    /// 进入
    /// </summary>
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
    }

    /// <summary>
    /// 退出
    /// </summary>
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public virtual void LogicUpdate() { }

    /// <summary>
    /// 物理更新
    /// </summary>
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    /// <summary>
    /// 检查
    /// </summary>
    public virtual void DoChecks() { }

    /// <summary>
    /// 动画触发
    /// </summary>
    public virtual void AnimationTrigger() { }

    /// <summary>
    /// 动画结束触发
    /// </summary>
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
