using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPartScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            // Give player 1 key
            other.GetComponent<KeyController>().keyCollected += 1;
            Destroy(this.gameObject);
        }
    }
}
