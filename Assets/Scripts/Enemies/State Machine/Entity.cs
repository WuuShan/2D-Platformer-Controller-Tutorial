using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体
/// </summary>
public class Entity : MonoBehaviour
{
    /// <summary>
    /// 状态机
    /// </summary>
    public FiniteStateMachine stateMachine;

    /// <summary>
    /// 实体数据
    /// </summary>
    public D_Entity entityData;
    /// <summary>
    /// 面对方向
    /// </summary>
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    /// <summary>
    /// 上次伤害方向
    /// </summary>
    public int lastDamageDirection { get; private set; }

    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    [SerializeField] private Transform wallCheck;
    /// <summary>
    /// 高角检查坐标
    /// </summary>
    [SerializeField] private Transform ledgeCheck;
    /// <summary>
    /// 玩家检查坐标
    /// </summary>
    [SerializeField] private Transform playerCheck;
    /// <summary>
    /// 地面检查坐标
    /// </summary>
    [SerializeField] private Transform groundCheck;

    /// <summary>
    /// 当前生命值
    /// </summary>
    private float currentHealth;
    /// <summary>
    /// 当前眩晕抗性
    /// </summary>
    private float currentStunResistance;
    /// <summary>
    /// 上次伤害时间
    /// </summary>
    private float lastDamageTime;

    /// <summary>
    /// 速度区间
    /// </summary>
    private Vector2 velocityWorkspace;

    /// <summary>
    /// 是否已眩晕
    /// </summary>
    protected bool isStunned;
    /// <summary>
    /// 是否死亡
    /// </summary>
    protected bool isDead;

    public virtual void Start()
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", rb.velocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="velocity">速度</param>
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    /// <summary>
    /// 设置矢量
    /// </summary>
    /// <param name="velocity">速度</param>
    /// <param name="angle">角度</param>
    /// <param name="direction">方向</param>
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    /// <summary>
    /// 检查墙壁
    /// </summary>
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    /// <summary>
    /// 检查高角
    /// </summary>
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    /// <summary>
    /// 检查地面
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    /// <summary>
    /// 在最小仇恨范围内检查玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 在最大仇恨范围内检查玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 检查近程动作中的玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 根据击飞高度击飞
    /// </summary>
    /// <param name="velocity">击飞高度</param>
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    /// <summary>
    /// 根据攻击详情受到伤害
    /// </summary>
    /// <param name="attackDetails">来自攻击者的攻击详情</param>
    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    /// <summary>
    /// 翻转
    /// </summary>
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAggroDistance), 0.2f);
    }
}
