using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoerrScriptt : MonoBehaviour
{
    public float timeToOpenFrames;
    public float degrees;
    public Vector3 upleftforward;

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
            for (int i = 0; i < timeToOpenFrames; i++)
            {

                yield return new WaitForFixedUpdate();
                transform.RotateAround(gongjern.position, upleftforward, degrees / timeToOpenFrames);

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
            for (int i = 0; i < timeToOpenFrames; i++)
            {

                yield return new WaitForFixedUpdate();
                transform.RotateAround(gongjern.position, upleftforward, -degrees / timeToOpenFrames);

            }
            closed = true;
        }
    }
}
