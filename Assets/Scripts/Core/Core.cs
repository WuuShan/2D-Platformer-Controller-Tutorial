using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用来管理各种核心组件
/// </summary>
public class Core : MonoBehaviour
{
    /// <summary>
    /// 处理刚体移动
    /// </summary>
    public Movement Movement
    {
        get => GenericNotInplementedError<Movement>.TryGet(movement, transform.parent.name);
        private set => movement = value;
    }
    /// <summary>
    /// 处理碰撞检查
    /// </summary>
    public CollisionSenses CollisionSenses
    {
        get => GenericNotInplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
        private set => collisionSenses = value;
    }
    /// <summary>
    /// 处理战斗相关
    /// </summary>
    public Combat Combat
    {
        get => GenericNotInplementedError<Combat>.TryGet(combat, transform.parent.name);
        private set => combat = value;
    }
    /// <summary>
    /// 处理数据统计
    /// </summary>
    public Stats Stats
    {
        get => GenericNotInplementedError<Stats>.TryGet(stats, transform.parent.name);
        private set => stats = value;
    }

    /// <summary>
    /// 移动组件
    /// </summary>
    private Movement movement;
    /// <summary>
    /// 碰撞感知组件
    /// </summary>
    private CollisionSenses collisionSenses;
    /// <summary>
    /// 战斗组件
    /// </summary>
    private Combat combat;
    /// <summary>
    /// 数据统计组件
    /// </summary>
    private Stats stats;

    private List<ILogicUpdate> components = new List<ILogicUpdate>();

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        Stats = GetComponentInChildren<Stats>();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void LogicUpdate()
    {
        foreach (ILogicUpdate component in components)
        {
            component.LogicUpdate();
        }
    }

    /// <summary>
    /// 添加拥有逻辑更新的组件
    /// </summary>
    /// <param name="component">组件</param>
    public void AddComponent(ILogicUpdate component)
    {
        if (!components.Contains(component))    // 判断列表是否有该组件
        {
            components.Add(component);
        }
    }
}
