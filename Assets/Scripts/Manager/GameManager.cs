using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// 游戏管理器
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 复活点
    /// </summary>
    [SerializeField] private Transform respawnPoint;
    /// <summary>
    /// 玩家
    /// </summary>
    [SerializeField] private GameObject player;
    /// <summary>
    /// 复活时间
    /// </summary>
    [SerializeField] private float respawnTime;

    /// <summary>
    /// 上次复活时间
    /// </summary>
    private float respawnTimeStart;
    /// <summary>
    /// 复活
    /// </summary>
    private bool respawn;

    private CinemachineVirtualCamera CVC;

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRespawn();
    }

    /// <summary>
    /// 复活
    /// </summary>
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    /// <summary>
    /// 检查复活
    /// </summary>
    private void CheckRespawn()
    {
        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }
}
