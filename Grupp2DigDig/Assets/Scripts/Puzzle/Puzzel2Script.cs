using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzel2Script : MonoBehaviour
{

    public GameObject hingeJärn;
    

    public void OpenDaDoor()
    {
        StartCoroutine(OpenDoorNumerator());
    }

    IEnumerator OpenDoorNumerator()
    {


        for (int i = 0; i < 102; i++)
        {
            transform.RotateAround(hingeJärn.transform.position, Vector3.up, 1);

            yield return new WaitForFixedUpdate();

        }
    }
}
