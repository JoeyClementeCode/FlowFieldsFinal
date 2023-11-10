using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private float attackTimer = 2.0f;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("This is the attacking state");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            //attack nexus
            attackTimer = 2.0f;
        }
        
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }
}
