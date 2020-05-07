using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoerrScriptt : MonoBehaviour
{
    Animator anim;

    bool closed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        closed = true;
        anim.enabled = false;
    }

    public void OpenDoor()
    {
        StartCoroutine(Open());
    }
    public void CloseDoor()
    {
        StartCoroutine(Close());
    }

    IEnumerator Open()
    {
        if (closed)
        {

            yield return new WaitForFixedUpdate();
            anim.enabled = true;

            closed = false;
        }
    }


    IEnumerator Close()
    {
        if (!closed)
        {

            yield return new WaitForFixedUpdate();
            anim.enabled = false;

            closed = true;
        }
    }
}
