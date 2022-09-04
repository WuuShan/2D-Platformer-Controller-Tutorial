using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发射物
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// 攻击详情
    /// </summary>
    // private AttackDetails attackDetails;

    /// <summary>
    /// 速度
    /// </summary>
    private float speed;
    /// <summary>
    /// 行程距离
    /// </summary>
    private float travelDistance;
    /// <summary>
    /// x起点位置
    /// </summary>
    private float xStartPos;

    /// <summary>
    /// 重力
    /// </summary>
    [SerializeField] private float gravity;
    /// <summary>
    /// 伤害范围
    /// </summary>
    [SerializeField] private float damageRadius;

    private Rigidbody2D rb;

    /// <summary>
    /// 重力是否开启
    /// </summary>
    private bool isGravityOn;
    /// <summary>
    /// 已命中地面
    /// </summary>
    private bool hasHitGround;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    /// <summary>
    /// 伤害坐标
    /// </summary>
    [SerializeField] private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            // attackDetails.position = transform.position;

            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                //damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    /// <summary>
    /// 发射开始
    /// </summary>
    /// <param name="speed">速度</param>
    /// <param name="travelDistance">行程距离</param>
    /// <param name="damage">伤害</param>
    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        //attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
