using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int keyCollected = 0;
    private bool messageSent = false;

    private void Update()
    {
        if (keyCollected == 2 && !messageSent)
        {
            //Tell player that they have 1 key
            messageSent = true;            
        }
    }

    // Handle player key ammount
}
