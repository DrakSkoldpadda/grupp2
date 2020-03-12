using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafFallScript : MonoBehaviour
{
    Transform PlayerTransform;
    public float distance;
    private ParticleSystem PS;

    private void Start()
    {
        PS = GetComponentInChildren<ParticleSystem>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Swag());
    }


    public IEnumerator Swag()
    {
        yield return new WaitForSeconds(0.5f);

        float distanceToPlayer = Vector3.Distance(PlayerTransform.position, transform.position);

        if (distanceToPlayer > distance)
        {
            PS.Stop();
        }

        else
            PS.Play();


        StartCoroutine(Swag());
    }
}
