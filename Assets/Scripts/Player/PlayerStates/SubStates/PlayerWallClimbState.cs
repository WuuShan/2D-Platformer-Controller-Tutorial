using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家爬墙状态
/// </summary>
public class PlayerWallClimbState : PlayerToucingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement.SetVelocityY(playerData.wallClimbVelocity);

            if (yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
