using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankCollidersScript : MonoBehaviour
{

    public GameObject Plankan;
    public GameObject magicball;

    public bool backwards;

    private void OnTriggerEnter(Collider other)
    {
        if (!backwards)
            Plankan.GetComponent<plankrotator>().RotateHelper(true);
        else
            Plankan.GetComponent<plankrotator>().RotateHelper(false);

        StartCoroutine(GoAway());
    }

    IEnumerator GoAway()
    {

        for (float i = 0; i < 2000; i++)
        {
            transform.Translate(0, i/2000f, 0);

            yield return new WaitForFixedUpdate();
        }
        Destroy(magicball);

    }
}
