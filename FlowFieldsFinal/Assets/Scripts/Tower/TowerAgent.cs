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
    private List<GameObject> currentEnemiesInRange = new List<GameObject>();
    private GameObject currentEnemy;
    public TowerTargetPriority targetPriority;

    [Header("Attacking")]
    public float attackRate;
    private float lastAttackTime;
    public Projectile projectilePrefab;
    public Transform projectileSpawnPos;
    public int projectileDamage;
    public float projectileSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    private GameObject GetEnemy()
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
            currentEnemiesInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            currentEnemiesInRange.Remove(other.gameObject);
        }
    }
}
