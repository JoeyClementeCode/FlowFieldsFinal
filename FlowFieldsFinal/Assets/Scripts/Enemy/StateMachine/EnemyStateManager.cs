using UnityEngine;

public  class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState currentState;

    public EnemyAttackingState attackingState = new EnemyAttackingState();
    public EnemyMovingState movingState = new EnemyMovingState();

    private void Start()
    {
        currentState = movingState;
        
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;

        state.EnterState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }
}
