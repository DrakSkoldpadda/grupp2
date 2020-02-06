using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighted : MonoBehaviour
{
    private Transform lamp;
    [SerializeField] private float activtedRange;
    [SerializeField] private bool activated;

    private void Awake()
    {
        lamp = GameObject.FindWithTag("lykta").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(lamp.position, transform.position) < activtedRange)
        {
            activated = true;
        }
        else
        {
            activated = false;
        }
    }
}
