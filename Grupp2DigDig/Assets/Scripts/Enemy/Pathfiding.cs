using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfiding : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private Transform target;

    [SerializeField] private float aggroRage = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < aggroRage)
        {
            agent.SetDestination(target.position);
        }
        else
        {

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRage);
    }
}