using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float regenRate;
    
    [SerializeField] private ObjectiveHealthBar healthBar;


    private int maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        
        healthBar.GetComponentInChildren<ObjectiveHealthBar>();
        
        healthBar.UpdateHealthbar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //regen when not being hit after x amount of time
    }
    
    
    public void ObjectiveTakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthbar(health, maxHealth);

        if (health <= 0)
        {
            //GameManager.instance.economy.GainCurrency(Value);
            Destroy(this.gameObject);
        }
    }
}
