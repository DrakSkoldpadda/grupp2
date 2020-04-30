using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]

public class PusselFlygandePlatta : MonoBehaviour
{

    public PusselFlygandeStone koppladSten;
    public AudioSource stenLjud;
    public float hurMktNer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlattaNer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlattaUpp());
        }
    }

    IEnumerator PlattaNer()
    {
        yield return new WaitForSeconds(0.4f);

        stenLjud.Play();

        for (int i = 0; i < 60; i++)
        {
            transform.Translate(new Vector3(0, -hurMktNer / 100, 0));
            yield return new WaitForFixedUpdate();
        }

        stenLjud.Stop();

        koppladSten.GoUp();
    }

    IEnumerator PlattaUpp()
    {
        yield return new WaitForSeconds(0.4f);

        stenLjud.Play();

        for (int i = 0; i < 60; i++)
        {
            transform.Translate(new Vector3(0, hurMktNer / 100, 0));
            yield return new WaitForFixedUpdate();
        }

        stenLjud.Stop();

        koppladSten.GoDown();
    }
    

}
