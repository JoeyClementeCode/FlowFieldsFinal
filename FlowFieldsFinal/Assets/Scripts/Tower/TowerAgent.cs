using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAgent : MonoBehaviour
{
    /*
     *  Lock onto ONLY ENEMIES within a radius
     *  After locking, shoot projectile from the top of the tower in the direction of the enemy with a constant force
     *  Projectiles are homing
     */

    public enum TowerTargetPriority
    {
        First,
        Close,
        Strong
    }
    [Header("Info")]
    public float range;
    public List<Enemy> currentEnemiesInRange = new List<Enemy>();
    private Enemy currentEnemy;
    public TowerTargetPriority targetPriority;

    [Header("Attacking")]
    public float attackRate;
    private float lastAttackTime;
    public Projectile projectilePrefab;
    public Transform projectileSpawnPos;
    public int projectileDamage;
    public float projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            currentEnemy = GetEnemy();
            if(currentEnemy != null)
                Attack();
        }
    }

    private Enemy GetEnemy()
    {
        currentEnemiesInRange.RemoveAll(x => x == null);
        
        if(currentEnemiesInRange.Count == 0)
            return null;
        if(currentEnemiesInRange.Count == 1)
            return currentEnemiesInRange[0];

        switch (targetPriority)
        {
            case TowerTargetPriority.First:
            {
                return currentEnemiesInRange[0];
            }
            case TowerTargetPriority.Close:
            {
                Enemy closest = null;
                float dist = 99;
                foreach (var enemy in currentEnemiesInRange)
                {
                    float d = (transform.position - enemy.transform.position).sqrMagnitude;
                    if(d < dist)
                    {
                        closest = enemy;
                        dist = d;
                    }
                }
                return closest;
            }
            case TowerTargetPriority.Strong:
            {
                Enemy strongest = null;
                int strongestHealth = 0;
                foreach(Enemy enemy in currentEnemiesInRange)
                {
                    if(enemy.Health > strongestHealth)
                    {
                        strongest = enemy;
                        strongestHealth = enemy.Health;
                    }
                }
                return strongest;
            }
        }

        return null;
    }
    
    private void Attack()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity);
        projectile.currentTarget = currentEnemy.transform;
        projectile.damage = projectileDamage;
        projectile.speed = projectileSpeed;
    }
    
    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            currentEnemiesInRange.Add(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            currentEnemiesInRange.Remove(other.GetComponent<Enemy>());
        }
    }
}
