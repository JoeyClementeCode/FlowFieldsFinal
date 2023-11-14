using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Checks")]
    [SerializeField] private List<GameObject> enemiesToSpawn = new List<GameObject>();
    [SerializeField] private float spawnTimer;
    [SerializeField] private bool isWaveActive = false;
    [SerializeField] private bool inUpgrades = false;
    public int enemyCount;
    public int maxEnemies;
    public int currentWave = 0;
    [SerializeField] private int waveAmount = 0;

    [Space(5)]
    
    [Header("Public Variables")] 
    public List<GameObject> wavePoints = new List<GameObject>();
    public List<EnemyCore> enemies = new List<EnemyCore>();
    public int waves = 50;
    public float enemySpawnSpeed = 20;
    public int waveAmountMultiplier = 10;
    public float timeBeforeNextWave = 100;
    

    public void StartWaves()
    {
        currentWave = 1;
        GenerateWave();
        isWaveActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaveActive)
        {
            if (spawnTimer <= 0 && (currentWave != waves))
            {
                if (enemiesToSpawn.Count > 0)
                {
                    SpawnEnemies();
                    spawnTimer = enemySpawnSpeed;
                }
                else
                {
                    isWaveActive = false;
                    Debug.Log("No Enemies To Spawn");
                }

            }
            else
            {
                spawnTimer -= Time.time;
            }
        }
        else
        {
            if (enemyCount == 0)
            {
                if (!inUpgrades)
                {
                    if (timeBeforeNextWave <= 0)
                    {
                        if (currentWave != 0)
                        {
                            //GameManager.Instance.enemyManager.ScaleStats();
                        }

                        currentWave++;
                        GenerateWave();
                        isWaveActive = true;
                        timeBeforeNextWave = 5;
                    }

                    timeBeforeNextWave -= Time.time;
                }
            }
        }
        

    }

    private void GenerateWave()
    {
        waveAmount = currentWave * waveAmountMultiplier;
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        Debug.Log(waveAmount);
        List<GameObject> generatedEnemies = new List<GameObject>();
        int currentEnemyAmount = 0;
        while (waveAmount > 0)
        {
            int enemyID = Random.Range(0, enemies.Count);
            int enemyCost = enemies[enemyID].cost;

            if (waveAmount - enemyCost >= 0)
            {
                generatedEnemies.Add(enemies[enemyID].enemyPrefab);
                currentEnemyAmount++;
                Debug.Log("Enemy: " + currentEnemyAmount + " added");
                waveAmount -= enemyCost;
            }
            else if (waveAmount <= 0)
            {
                break;
            }
        }
        
        enemiesToSpawn.Clear();
        maxEnemies = currentEnemyAmount;
        enemyCount = maxEnemies;
        enemiesToSpawn = generatedEnemies;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < wavePoints.Count; i++)
        {
            if (enemiesToSpawn.Count > 0)
            {
                GameManager.instance.enemyManager.SpawnEnemy(enemiesToSpawn[0], wavePoints[i].transform.position);
                enemiesToSpawn.RemoveAt(0);
            }
        }
    }
    
}

[System.Serializable]
public class EnemyCore
{
    public GameObject enemyPrefab;
    public int cost;
}

