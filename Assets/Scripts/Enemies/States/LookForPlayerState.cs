using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 寻找玩家状态
/// </summary>
public class LookForPlayerState : State
{
    private Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;

    /// <summary>
    /// 状态数据
    /// </summary>
    protected D_LookForPlayer stateData;

    /// <summary>
    /// 立即转身
    /// </summary>
    protected bool turnImmediately;
    /// <summary>
    /// 玩家是否在最小仇恨范围内
    /// </summary>
    protected bool isPlayerInMinAggroRange;
    /// <summary>
    /// 全部转身是否结束
    /// </summary>
    protected bool isAllTurnsDone;
    /// <summary>
    /// 全部转身时间是否结束
    /// </summary>
    protected bool isAllTurnsTimeDone;

    /// <summary>
    /// 上次转身时间
    /// </summary>
    protected float lastTurnTime;
    /// <summary>
    /// 完成的转身次数
    /// </summary>
    protected int amountOfTurnsDone;


    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        Movement.SetVelocityX(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement.SetVelocityX(0);

        if (turnImmediately)
        {
            Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    /// <summary>
    /// 设置立即翻转
    /// </summary>
    /// <param name="turn"></param>
    public void SetTurnImmediately(bool turn)
    {
        turnImmediately = turn;
    }
}
