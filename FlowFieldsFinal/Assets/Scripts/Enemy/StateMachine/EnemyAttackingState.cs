using System.Collections;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private float attackTimer = 2.0f;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("This is the attacking state");
        
        enemy.GetComponent<Enemy>().SetTarget(null);

    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            //attack nexus
            GameManager.instance.objectiveManager.ObjectiveTakeDamage(1);
            Debug.Log("Damage done");
            attackTimer = 10.0f;
        }
        // if (GameManager.instance.objectiveManager.ObjectiveHealth < 10)
        // {
        //     //LOAD LOSE STATE
        // }
        
    }
    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }

    public override void OnTriggerEnter(EnemyStateManager enemy, Collider other)
    {
    }
}
