using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家残影
/// </summary>
public class PlayerAfterImageSprite : MonoBehaviour
{
    /// <summary>
    /// 存在时间
    /// </summary>
    [SerializeField] private float activeTime = 0.1f;
    /// <summary>
    /// 激活时间
    /// </summary>
    private float timeActivated;
    /// <summary>
    /// 透明度
    /// </summary>
    private float alpha;
    /// <summary>
    /// 透明度设置
    /// </summary>
    [SerializeField] private float alphaSet = 0.8f;
    /// <summary>
    /// 透明度倍数
    /// </summary>
    [SerializeField] private float alphaDecay = 0.85f;

    /// <summary>
    /// 玩家坐标
    /// </summary>
    private Transform player;

    /// <summary>
    /// 图片组件
    /// </summary>
    private SpriteRenderer SR;
    /// <summary>
    /// 玩家图片组件
    /// </summary>
    private SpriteRenderer playerSR;

    /// <summary>
    /// 颜色
    /// </summary>
    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            // Add back to pool.重新添加到池中。
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
