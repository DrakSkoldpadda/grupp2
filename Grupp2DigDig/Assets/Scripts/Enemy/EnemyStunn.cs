using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunn : MonoBehaviour
{
    [SerializeField] private float stunnDuration = 0.7f;
    private float stunnTimeLeft;

    private Pathfiding AI;

    private void Awake()
    {
        AI = GetComponentInParent<Pathfiding>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stunnTimeLeft = stunnDuration;
        }
    }

    private void Update()
    {
        if (stunnTimeLeft > 0)
        {
            AI.agent.isStopped = true;
            stunnTimeLeft -= Time.deltaTime;
        }
        else
        {
            AI.agent.isStopped = false;
        }
    }
}
