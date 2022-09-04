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

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Combat combat;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
    }
}
