using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoerrScriptt : MonoBehaviour
{
    public float timeToOpen;
    Vector3 rotateAroundThisPoint;
    // Start is called before the first frame update
    void Start()
    {
        rotateAroundThisPoint = GetComponentInChildren<Transform>().position;
        StartCoroutine(Open());
    }

    public void OpenDoor()
    {
        StartCoroutine(Open());
    }
    public void CloseDoor()
    {

    }

    IEnumerator Open()
    {
        for (int i = 0; i < timeToOpen; i++)
        {

            yield return new WaitForFixedUpdate();
            transform.RotateAround(rotateAroundThisPoint, Vector3.up, 70f / timeToOpen); 
            
            // gör sp den öppnas ordentligt med rätt rotation
            //fixa länkningen mellan plattan och dörren open / close.
            
        }
    }


    IEnumerator Close()
    {
        yield return new WaitForFixedUpdate(); 
    }
}
