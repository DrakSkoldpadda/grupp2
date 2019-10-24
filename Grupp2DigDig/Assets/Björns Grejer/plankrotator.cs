using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plankrotator : MonoBehaviour
{
    public float speed;

    bool ready;

    private void Start()
    {
        ready = true;
    }

    public int stopNumber;
    int counter;
    IEnumerator RotateToPLayer()
    {
        print("Forwardstime");
        yield return new WaitForFixedUpdate();
        transform.Rotate(0, speed, 0);

        counter++;
        if (stopNumber > counter)
        {
            StartCoroutine(RotateToPLayer());
        }
        else
        {
            StopCoroutine(RotateToPLayer());
            ready = true;
        }
    }

    IEnumerator RotateToPLayerBackwards()
    {
        print("backwardstime");
        yield return new WaitForFixedUpdate();
        transform.Rotate(0, -speed, 0);

        counter++;
        if (-stopNumber > counter)
        {
            StartCoroutine(RotateToPLayerBackwards());
        }
        else
        {
            StopCoroutine(RotateToPLayerBackwards());
            ready = true;
        }
    }

    public void RotateHelper(bool ForwardTrue)
    {
        if (ready == true)
            if (ForwardTrue)
            {
                StartCoroutine(RotateToPLayer());
                ready = false;
            }
            else
            {
                StartCoroutine(RotateToPLayerBackwards());
                ready = false;
            }
    }
}
