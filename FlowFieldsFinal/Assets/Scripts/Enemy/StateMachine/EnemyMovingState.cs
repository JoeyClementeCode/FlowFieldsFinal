using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("This is the moving state");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //enemy.SwitchState(enemy.attackingState);
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
    }
    
    
    public override void OnTriggerEnter(EnemyStateManager enemy, Collider other)
    {
        GameObject c = other.gameObject;

        if (c.CompareTag("Player"))
        {
            Debug.Log("hit player");

            //other.GetComponent<PlayerController>().LoseHealth;
        }
        
        if (c.CompareTag("Objective"))
        {
            enemy.SwitchState(enemy.attackingState);
            Debug.Log("switching to the attacking state!");
            //other.GetComponent<Nexus>().LoseHealth;
        }
    }
}
