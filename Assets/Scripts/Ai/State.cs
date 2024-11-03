using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class State
{
    public UnityEvent OnStageChanged;

    public enum STATE
    {
        PATROL,
        CHASE,
        ATTACK
    }

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    public STATE Name; //state ���������
    protected EVENT Stage; //���ѧ����������� state ���
    protected State NextState; //��� state �˹���

    protected Enemy Me;
    protected NavMeshAgent Agent;
    protected EnemyGun Gun;

    public State(Enemy enemy , NavMeshAgent agent, EnemyGun gun)
    {
        Me = enemy;
        Agent = agent;
        Gun = gun;
        Stage = EVENT.ENTER;
        
    }
    /// <summary>
    /// What to do enter this stage
    /// </summary>
    public virtual void Enter()
    {
        Stage = EVENT.UPDATE;
    }
    /// <summary>
    /// What to do when is in this stage. Including condition to change stage.
    /// *You need to implement Stage = EVENT EXIT    
    /// </summary>
    public virtual void Update()
    {
        Stage = EVENT.UPDATE;
    }
    /// <summary>
    /// What to do on exit stage
    /// </summary>
    public virtual void Exit()
    {
        Stage = EVENT.UPDATE;
    }
    public State Process()
    {
        if (Stage == EVENT.ENTER)
        {
            Enter();
        }
        if (Stage == EVENT.UPDATE)
        {
            Update();
        }
        if(Stage == EVENT.EXIT)
        {
            Exit();
            OnStageChanged?.Invoke();
            return NextState;
        }
        return this;
    }
}
