using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家跳跃状态
/// </summary>
public class PlayerJumpState : PlayerAbilityState
{
    /// <summary>
    /// 剩余跳跃次数
    /// </summary>
    private int amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        Movement.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    /// <summary>
    /// 能否跳跃
    /// </summary>
    /// <returns></returns>
    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 重置剩余跳跃次数
    /// </summary>
    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    /// <summary>
    /// 减少剩余跳跃次数
    /// </summary>
    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
