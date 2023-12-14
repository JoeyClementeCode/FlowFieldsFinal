using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    
    [SerializeField] private FloatingHealthBar healthBar;
    
    public int Health { get; set; }

    private int MaxHealth;
    public int Value { get; set; }

    public void Start()
    {
        Health = 3;
        MaxHealth = Health;
        Value = 5;

        agent = this.GetComponent<NavMeshAgent>();
        healthBar.GetComponentInChildren<FloatingHealthBar>();
        
        healthBar.UpdateHealthbar(Health, MaxHealth);
    }

    private void Update()
    {
        if (target != null && this != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthBar.UpdateHealthbar(Health, MaxHealth);

        if (Health <= 0)
        {
            //GameManager.instance.economy.GainCurrency(Value);
            Destroy(this.gameObject);
        }
    }

    public void SetTarget(Transform new_target)
    {
        target = new_target;
    }
}
