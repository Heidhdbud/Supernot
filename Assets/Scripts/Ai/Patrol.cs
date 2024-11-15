using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    public Patrol(Enemy enemy, NavMeshAgent agent, EnemyGun gun) : base(enemy, agent, gun)
    {
        Name = STATE.CHASE;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (Agent.remainingDistance <= Agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(Me.transform.position, Me.walkPointRange, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                Agent.SetDestination(point);
            }
        }

        if (Me.playerInChaseRange && !Me.playerInAttackRange)
        {
            NextState = new Chase(Me, Agent, Gun);
            Stage = EVENT.EXIT;
        }
        if (Me.playerInChaseRange && Me.playerInAttackRange)
        {
            NextState = new Attack(Me, Agent, Gun);
            Stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
