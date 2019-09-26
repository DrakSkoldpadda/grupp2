using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCam : MonoBehaviour
{
    private Transform cameraPosition;

    private void Start()
    {
        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (cameraPosition != null)
        {
            transform.position = cameraPosition.position;
            transform.rotation = Quaternion.Euler(0, cameraPosition.eulerAngles.y , 0);
        }
        else
        {
            Debug.LogError("There is no camera posiotion");
        }
    }

}
