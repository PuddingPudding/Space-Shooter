using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform m_tEnemySpawnPoint;
    [SerializeField] private Vector2 m_v2EnemyInterval;
    private static EnemySpawner g_instance = null;

    private void Start()
    {
        EnemySpawner.g_instance = this;
    }

    public static EnemySpawner GetInstance()
    {
        if(EnemySpawner.g_instance == null)
        {
            EnemySpawner.g_instance = new EnemySpawner();
        }
        return EnemySpawner.g_instance;
    }

    private EnemySpawner()
    {
    }
}
