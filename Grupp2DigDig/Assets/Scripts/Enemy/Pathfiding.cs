using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfiding : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;

    private Transform target;

    [SerializeField] private float aggroRange = 3.5f;

    [SerializeField] private float moveSpeed = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Start()
    {
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < aggroRange)
        {
            agent.SetDestination(target.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}