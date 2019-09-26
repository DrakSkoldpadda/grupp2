using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Application.Quit();
        }
    }
}
