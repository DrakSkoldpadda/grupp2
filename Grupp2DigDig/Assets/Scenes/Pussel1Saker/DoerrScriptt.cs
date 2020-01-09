using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoerrScriptt : MonoBehaviour
{
    public float timeToOpen;
    public Transform gongjern;
    bool closed;
    // Start is called before the first frame update
    void Start()
    {
        closed = true;
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
            for (int i = 0; i < timeToOpen; i++)
            {

                yield return new WaitForFixedUpdate();
                transform.RotateAround(gongjern.position, Vector3.up, 90f / timeToOpen);

                // gör sp den öppnas ordentligt med rätt rotation
                //fixa länkningen mellan plattan och dörren open / close.

            }
            closed = false;
        }
    }


    IEnumerator Close()
    {
        if (!closed)
        {
            for (int i = 0; i < timeToOpen; i++)
            {

                yield return new WaitForFixedUpdate();
                transform.RotateAround(gongjern.position, Vector3.up, -90f / timeToOpen);

            }
            closed = true;
        }
    }
}
