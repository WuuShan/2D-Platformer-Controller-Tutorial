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
        get
        {
            if (movement)
            {
                return movement;
            }

            Debug.LogError("No Movement Core Component on " + transform.parent.name);
            return null;
        }

        private set { movement = value; }
    }
    /// <summary>
    /// 处理碰撞检查
    /// </summary>
    public CollisionSenses CollisionSenses
    {
        get
        {
            if (collisionSenses)
            {
                return collisionSenses;
            }

            Debug.LogError("No collision Senses Core Component on " + transform.parent.name);
            return null;
        }
        private set { collisionSenses = value; }
    }

    private Movement movement;
    private CollisionSenses collisionSenses;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
