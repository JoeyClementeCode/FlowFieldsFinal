using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    
    public int Health { get; set; }
    public int Value { get; set; }

    public void Start()
    {
        Health = 3;
        Value = 5;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination((target.position));
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            GameManager.instance.economy.GainCurrency(Value);
            Destroy(this.gameObject);
        }
    }
}
