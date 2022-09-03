using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// 武器数据
    /// </summary>
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState state;

    /// <summary>
    /// 攻击计数器
    /// </summary>
    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入武器攻击
    /// </summary>
    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);

        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }

    /// <summary>
    /// 退出武器攻击
    /// </summary>
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// 初始化武器的玩家攻击状态
    /// </summary>
    /// <param name="state">玩家攻击状态</param>
    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }

    #region Animation Triggers

    /// <summary>
    /// 结束时触发
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    /// <summary>
    /// 开始移动触发
    /// </summary>
    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    /// <summary>
    /// 停止移动触发
    /// </summary>
    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    /// <summary>
    /// 关闭翻转
    /// </summary>
    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    /// <summary>
    /// 启动翻转
    /// </summary>
    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {

    }

    #endregion

}
