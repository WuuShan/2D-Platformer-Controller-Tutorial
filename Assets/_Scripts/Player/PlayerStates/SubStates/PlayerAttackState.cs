using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家攻击状态
/// </summary>
public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
}

