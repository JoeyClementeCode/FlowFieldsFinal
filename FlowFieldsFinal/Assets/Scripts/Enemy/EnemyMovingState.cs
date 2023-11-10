using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("This is the moving state");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        enemy.SwitchState(enemy.attackingState);
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().LoseHealth;
        }
        
        if (other.CompareTag("Nexus"))
        {
            enemy.SwitchState(enemy.attackingState);
            //other.GetComponent<Nexus>().LoseHealth;
        }
    }
}
