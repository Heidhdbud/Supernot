using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static State;
using UnityEngine.AI;

public class Chase : State
{
    public Chase(Enemy enemy, NavMeshAgent agent, EnemyGun gun) : base(enemy, agent, gun)
    {
        Name = STATE.CHASE;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        Agent.SetDestination(Me._player.position);
        if (!Me.playerInChaseRange && !Me.playerInAttackRange)
        {
            NextState = new Patrol(Me, Agent, Gun);
            Stage = EVENT.EXIT;
        }
        if (Me.playerInChaseRange && Me.playerInAttackRange)
        {
            NextState = new Attack(Me, Agent , Gun);
            Stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
