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

    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    [SerializeField] private Transform wallCheck;
    /// <summary>
    /// 墙壁检查坐标
    /// </summary>
    [SerializeField] private Transform ledgeCheck;
    /// <summary>
    /// 玩家检测坐标
    /// </summary>
    [SerializeField] private Transform playerCheck;

    /// <summary>
    /// 速度区间
    /// </summary>
    private Vector2 velocityWorkspace;

    public virtual void Start()
    {
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="velocity"></param>
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
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
    }
}
