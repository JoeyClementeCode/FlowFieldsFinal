using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health { get; set; }
    public int Value { get; set; }

    public void Start()
    {
        Health = 3;
        Value = 5;
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
