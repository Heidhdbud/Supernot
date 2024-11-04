using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform _player;
    NavMeshAgent _agent;

    [Header("Checking")]

    public bool playerInChaseRange;
    public bool playerInAttackRange;
    public float chaseRange;
    public float attackRange;
    public LayerMask player;

    [Header("Move")]
    [SerializeField] float speed;
    public float walkPointRange;
    [Header("State")]
    private State _currentState;

    [Header("Attack")]
    [SerializeField] EnemyGun thisGun;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new Patrol(this, _agent, thisGun);
    }
    private void Start()
    {
        _agent.speed = speed;
    }



    private void FixedUpdate()
    {
        _currentState = _currentState.Process();

        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

    }
    private void OnDrawGizmosSelected()
    {
        //Shows enemy sight range in Scene window
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        //Shows enemy attack range in Scene window
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //Show enemy random walk range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}
