using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private Transform objective;


    public void SpawnEnemy(GameObject enemy, Vector2 position)
    {
        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().SetTarget(objective);
        enemies.Add(newEnemy);
    }
    
    public void DeleteEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public void DeleteAll()
    {
        enemies.Clear();
    }
}
