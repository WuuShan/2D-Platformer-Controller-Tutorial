using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 用来管理各种核心组件
/// </summary>
public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

    private void Awake()
    {
        // Find all core component children
        // 查找所有核心组件子组件
        var comps = GetComponentsInChildren<CoreComponent>();

        // Add componets found to list. Use old function to avoid duplicates.
        // 将找到的组件添加到列表中。 使用旧功能避免重复。
        foreach (var component in comps)
        {
            AddComponent(component);
        }

        // Call Init on each
        // 调用每个Init
        foreach (var component in CoreComponents)
        {
            component.Init(this);
        }
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }

    /// <summary>
    /// 将核心组件添加到列表
    /// </summary>
    /// <param name="component">核心组件</param>
    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))    // 判断列表是否有该核心组件
        {
            CoreComponents.Add(component);
        }
    }

    /// <summary>
    /// 根据组件类型获得核心组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <returns></returns>
    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();    // 返回 T 类型集合中的第一个组件，若是长度为 0 则返回 null

        if (comp == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return comp;
    }

    /// <summary>
    /// 根据组件获得核心组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="value">组件</param>
    /// <returns></returns>
    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}
