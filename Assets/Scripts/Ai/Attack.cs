using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(Enemy enemy, NavMeshAgent agent, EnemyGun gun) : base(enemy, agent, gun)
    {
        Name = STATE.ATTACK;
    }

    public override void Enter()
    {
        base.Enter();
        Gun.readyToAttack = true;
    }
    public override void Update()
    {
        Agent.SetDestination(Me.transform.position);
        Gun.transform.LookAt(Me._player);
        Me.transform.LookAt(Me._player);
        
        
        if (!Me.playerInChaseRange && !Me.playerInAttackRange)
        {
            NextState = new Patrol(Me, Agent, Gun);
            Stage = EVENT.EXIT;
        }
        if (Me.playerInChaseRange && !Me.playerInAttackRange)
        {
            NextState = new Chase(Me, Agent, Gun);
            Stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        Gun.readyToAttack = false;
        base.Exit();
    }
}

    

