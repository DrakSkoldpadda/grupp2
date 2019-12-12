using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfiding : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;

    private Animator animator;
    private Rigidbody rbdy;

    private Transform target;

    [SerializeField] private float aggroRange = 3.5f;

    [SerializeField] private float moveSpeed = 10f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rbdy = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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

            //-----------------------------springa mot gubben
            agent.SetDestination(target.position);

            //-----------------------------springa ifrån gubben
            //agent.Move(new Vector3(
            //    Mathf.Clamp(gameObject.transform.position.x - target.transform.position.x, -1,1),
            //    Mathf.Clamp(gameObject.transform.position.y - target.transform.position.y, -1, 1),
            //    Mathf.Clamp(gameObject.transform.position.z - target.transform.position.z, -1, 1)
            //    ) * 0.05f);


            animator.SetFloat("speed", 1f);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}