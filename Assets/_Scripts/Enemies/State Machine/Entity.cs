using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体
/// </summary>
public class Entity : MonoBehaviour
{
    private Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }
    private Movement movement;

    /// <summary>
    /// 状态机
    /// </summary>
    public FiniteStateMachine stateMachine;

    /// <summary>
    /// 实体数据
    /// </summary>
    public D_Entity entityData;

    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    /// <summary>
    /// 上次伤害方向
    /// </summary>
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

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

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();

        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Movement.RB.velocity.y);

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
    /// 在最小仇恨范围内检查玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 在最大仇恨范围内检查玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 检查近程动作中的玩家
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 根据击飞高度击飞
    /// </summary>
    /// <param name="velocity">击飞高度</param>
    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }

    /// <summary>
    /// 重置眩晕抗性
    /// </summary>
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAggroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAggroDistance), 0.2f);

        }
    }
}
