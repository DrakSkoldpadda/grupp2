using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class svampLjud : MonoBehaviour
{
    public AudioClip[] ljud;

    public AudioSource source;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            source.clip = ljud[Random.Range(0, 6)];

            source.Play();
        }
    }
}
