using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家攻击状态
/// </summary>
public class PlayerAttackState : PlayerAbilityState
{
    /// <summary>
    /// 武器
    /// </summary>
    private Weapon weapon;

    /// <summary>
    /// X轴输入
    /// </summary>
    private int xInput;

    /// <summary>
    /// 要设置的速度
    /// </summary>
    private float velocityToSet;
    /// <summary>
    /// 设置速度
    /// </summary>
    private bool setVelocity;
    /// <summary>
    /// 检查应该翻转
    /// </summary>
    private bool shouldCheckFlip;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;

        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;

        if (shouldCheckFlip)
        {
            Movement?.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            Movement?.SetVelocityX(velocityToSet * Movement.FacingDirection);
        }
    }

    /// <summary>
    /// 设置攻击状态的武器
    /// </summary>
    /// <param name="weapon">武器</param>
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this, core);
    }

    /// <summary>
    /// 根据速度设置玩家X轴移动
    /// </summary>
    /// <param name="velocity">速度</param>
    public void SetPlayerVelocity(float velocity)
    {
        Movement?.SetVelocityX(velocity * Movement.FacingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    /// <summary>
    /// 设置翻转检查
    /// </summary>
    /// <param name="value"></param>
    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    #endregion
}

