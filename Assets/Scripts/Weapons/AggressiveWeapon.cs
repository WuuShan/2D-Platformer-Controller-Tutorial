using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击性武器
/// </summary>
public class AggressiveWeapon : Weapon
{
    /// <summary>
    /// 攻击性武器数据
    /// </summary>
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    /// <summary>
    /// 用于检测拥有可伤害接口列表
    /// </summary>
    private List<IDamageable> detectedDamageable = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    /// <summary>
    /// 对拥有可伤害接口的对象造成伤害
    /// </summary>
    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageable)
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }

}
