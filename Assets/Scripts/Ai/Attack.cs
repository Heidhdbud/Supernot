using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(Enemy enemy, NavMeshAgent agent) : base(enemy, agent)
    {
        Name = STATE.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        Agent.SetDestination(Me.transform.position);
        if (Me.playerInChaseRange && !Me.playerInAttackRange)
        {
            NextState = new Patrol(Me, Agent);
            Stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

    

