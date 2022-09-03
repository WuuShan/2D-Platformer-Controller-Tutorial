using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    /// <summary>
    /// 处理刚体移动
    /// </summary>
    public Movement Movement { get; private set; }
    /// <summary>
    /// 处理碰撞检查
    /// </summary>
    public CollisionSenses CollisionSenses { get; private set; }

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();

        if (!Movement || !CollisionSenses)
        {
            Debug.LogError("Missing Core Component");
        }
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}
