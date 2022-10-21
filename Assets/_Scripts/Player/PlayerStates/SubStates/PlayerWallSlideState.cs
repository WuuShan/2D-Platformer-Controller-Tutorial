using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家滑墙状态
/// </summary>
public class PlayerWallSlideState : PlayerToucingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement.SetVelocityY(-playerData.wallSlideVelocity);

            if (grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
