using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础敌人控制器
/// </summary>
public class BasicEnemyController : MonoBehaviour
{
    /// <summary>
    /// 状态
    /// </summary>
    private enum State
    {
        /// <summary>
        /// 移动
        /// </summary>
        Moving,
        /// <summary>
        /// 击退
        /// </summary>
        Knockback,
        /// <summary>
        /// 死亡
        /// </summary>
        Dead
    }

    /// <summary>
    /// 当前状态
    /// </summary>
    private State currentState;

    /// <summary>
    /// 检查距离
    /// </summary>
    [SerializeField] private float groundCheckDistance, wallCheckDistance;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField] private float movementSpeed;
    /// <summary>
    /// 最大生命值
    /// </summary>
    [SerializeField] private float maxHealth;
    /// <summary>
    /// 击退持续时间
    /// </summary>
    [SerializeField] private float knockbackDuration;
    /// <summary>
    /// 上次接触伤害时间
    /// </summary>
    [SerializeField] private float lastTouchDamageTime;
    /// <summary>
    /// 接触伤害冷却
    /// </summary>
    [SerializeField] private float touchDamageCooldown;
    /// <summary>
    /// 接触伤害
    /// </summary>
    [SerializeField] private float touchDamage;
    /// <summary>
    /// 接触伤害范围
    /// </summary>
    [SerializeField] private float touchDamageWidth, touchDamageHeight;
    /// <summary>
    /// 检查坐标
    /// </summary>
    [SerializeField] private Transform groundCheck, wallCheck, touchDamageCheck;
    /// <summary>
    /// 图层蒙版
    /// </summary>
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    /// <summary>
    /// 击退位移
    /// </summary>
    [SerializeField] private Vector2 knockbackSpeed;
    /// <summary>
    /// 粒子特效
    /// </summary>
    [SerializeField] private GameObject hitParticle, deathChunkParticle, deathBloodParticle;

    /// <summary>
    /// 当前生命值
    /// </summary>
    private float currentHealth;
    /// <summary>
    /// 击退计时
    /// </summary>
    private float knockbackStartTime;
    /// <summary>
    /// 伤害详情
    /// </summary>
    private float[] attackDetails = new float[2];

    /// <summary>
    /// 方向
    /// </summary>
    private int facingDirection, damageDirection;

    /// <summary>
    /// 移动向量
    /// </summary>
    private Vector2 movement;
    /// <summary>
    /// 接触伤害向量
    /// </summary>
    private Vector2 touchDamageBotLeft, touchDamageTopRight;

    /// <summary>
    /// 已检测
    /// </summary>
    private bool groundDetected, wallDetected;

    // Component
    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
            default:
                break;
        }
    }

    #region -- MOVING STATE ----------------------------------------------------------------------------
    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            // Flip
            Flip();
        }
        else
        {
            // Move
            movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }
    #endregion

    #region -- KNOCKBACK STATE --------------------------------------------------------------------------
    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }
    #endregion

    #region -- DEAD STATE -------------------------------------------------------------------------------
    private void EnterDeadState()
    {
        // Spawn chunks and blood
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }
    #endregion

    #region -- OTHER FUNCTIONS --------------------------------------------------------------------------

    /// <summary>
    /// 根据攻击详情造成伤害
    /// </summary>
    /// <param name="attackDetails"></param>
    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        // Hit particle

        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    /// <summary>
    /// 检查接触伤害
    /// </summary>
    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    /// <summary>
    /// 切换当前状态
    /// </summary>
    /// <param name="state"></param>
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
            default:
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
            default:
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

    #endregion
}