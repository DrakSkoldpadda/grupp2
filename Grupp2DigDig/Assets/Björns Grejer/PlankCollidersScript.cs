using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankCollidersScript : MonoBehaviour
{

    public GameObject Plankan;
    public bool backwards;

    private void OnTriggerEnter(Collider other)
    {
        if (!backwards)
            Plankan.GetComponent<plankrotator>().RotateHelper(true);
        else
            Plankan.GetComponent<plankrotator>().RotateHelper(false);

    }
}
