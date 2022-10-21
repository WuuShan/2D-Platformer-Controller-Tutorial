using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 核心组件基类
/// </summary>
public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core core;

    public virtual void Init(Core core)
    {
        this.core = core;
    }

    protected virtual void Awake()
    {
        //core = transform.parent.GetComponent<Core>();

        //if (core == null)
        //{
        //    Debug.LogError("There is no Core on the parent");
        //}

        //core.AddComponent(this);
    }

    public virtual void LogicUpdate()
    {

    }
}
