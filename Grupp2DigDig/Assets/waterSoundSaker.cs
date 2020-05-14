using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSoundSaker : MonoBehaviour
{

    public AudioSource vattenLjud;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            vattenLjud.Play();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            vattenLjud.Stop();  
        }
    }
}
