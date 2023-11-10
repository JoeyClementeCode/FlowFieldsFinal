using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform objective;

    [SerializeField] private float spawnDelay = 3.0f;

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnDelay;
        
        enemyPrefab.GetComponent<Enemy>().SetTarget(objective);
        
        Instantiate(enemyPrefab, this.transform.position, quaternion.identity);

    }

    private void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            // Bug: Pretty sure its the rotation, the agent doesnt move right when it spawns
            Instantiate(enemyPrefab, this.transform.position, quaternion.identity);
            spawnTimer = spawnDelay;
            
        }
    }
}
