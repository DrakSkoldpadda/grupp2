using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winScript : MonoBehaviour
{

    public AudioSource musikObjekt;

    public ParticleSystem particles;
    public AudioSource musik;
    public GameObject sphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            particles.Play();
            musik.Play();

            Destroy(sphere);

            musikObjekt.Stop();
        }
    }


}
