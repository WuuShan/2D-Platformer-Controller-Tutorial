using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家残影池
/// </summary>
public class PlayerAfterImagePool : MonoBehaviour
{
    /// <summary>
    /// 残影预制体
    /// </summary>
    [SerializeField] private GameObject afterImagePrefab;

    /// <summary>
    /// 可用对象队列
    /// </summary>
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    /// <summary>
    /// 单例模式
    /// </summary>
    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GetFromPool();
    }

    /// <summary>
    /// 生成池
    /// </summary>
    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    /// <summary>
    /// 添加到池
    /// </summary>
    /// <param name="instance"></param>
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    /// <summary>
    /// 从池中获取
    /// </summary>
    /// <returns></returns>
    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        var instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
