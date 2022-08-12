using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗假人控制器
/// </summary>
public class CombatDummyController : MonoBehaviour
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    [SerializeField] private float maxHealth;
    /// <summary>
    /// 击退位移
    /// </summary>
    [SerializeField] private float knockbackSpeedX, knockbackSpeedY;
    /// <summary>
    /// 击退方向
    /// </summary>
    [SerializeField] private float knockbackDuration;
    /// <summary>
    /// 击退死亡位移
    /// </summary>
    [SerializeField] private float knockbackDeathSpeedX, knockbackDeathSpeedY;
    /// <summary>
    /// 死亡扭矩
    /// </summary>
    [SerializeField] private float deathTorque;
    /// <summary>
    /// 应用击退
    /// </summary>
    [SerializeField] private bool applyKnockback;
    /// <summary>
    /// 命中粒子特效预制体
    /// </summary>
    [SerializeField] private GameObject hitParticle;

    /// <summary>
    /// 当前生命值
    /// </summary>
    private float currentHealth;
    /// <summary>
    /// 击退开始时间
    /// </summary>
    private float knockbackStart;

    /// <summary>
    /// 角色面向方向
    /// </summary>
    private int playerFacingDirection;

    /// <summary>
    /// 玩家在左边
    /// </summary>
    private bool playerOnLeft;
    /// <summary>
    /// 击退
    /// </summary>
    private bool knockback;

    // Component
    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    /// <summary>
    /// 根据伤害值减少对应生命值，并激活相应命中粒子、动画和位移
    /// </summary>
    /// <param name="details">伤害值</param>
    private void Damage(float[] details)
    {
        currentHealth -= details[0];

        if (details[1] < aliveGO.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else
        {
            playerFacingDirection = -1;
        }

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("PlayerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockback && currentHealth > 0.0f)
        {
            // Knockback
            Knockback();
        }

        if (currentHealth <= 0)
        {
            // Dead
            Die();
        }
    }

    /// <summary>
    /// 击退
    /// </summary>
    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    /// <summary>
    /// 检查击退
    /// </summary>
    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
