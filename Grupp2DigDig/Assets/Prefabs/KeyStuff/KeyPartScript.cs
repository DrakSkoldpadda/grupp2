using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPartScript : MonoBehaviour
{
    private AudioSource audio;

    private bool triggered = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("Player") && !triggered)
        {
            triggered = true;


            // Give player 1 key
            other.GetComponent<KeyController>().keyCollected += 1;

            audio.Play();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            Destroy(this.gameObject, 5);
        }
    }


    
}
