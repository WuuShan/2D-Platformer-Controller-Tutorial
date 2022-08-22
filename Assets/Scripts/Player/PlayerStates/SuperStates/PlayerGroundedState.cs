using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家地面状态
/// </summary>
public class PlayerGroundedState : PlayerState
{
    /// <summary>
    /// 横轴输入
    /// </summary>
    protected int xInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
