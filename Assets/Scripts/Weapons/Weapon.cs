using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器
/// </summary>
public class Weapon : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState state;

    protected virtual void Start()
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

        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);
    }

    /// <summary>
    /// 退出武器攻击
    /// </summary>
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        gameObject.SetActive(false);
    }


    #region Animation Triggers

    /// <summary>
    /// 动画结束时触发
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    #endregion

    /// <summary>
    /// 初始化武器的玩家攻击状态
    /// </summary>
    /// <param name="state">玩家攻击状态</param>
    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}
