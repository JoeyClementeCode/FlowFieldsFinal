using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private float attackTimer = 2.0f;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("This is the attacking state");
        
        //enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;

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
            Debug.Log("Damage done");
            //GameManager.instance.objectiveManager.ObjectiveTakeDamage(1);
            attackTimer = 2.0f;
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
