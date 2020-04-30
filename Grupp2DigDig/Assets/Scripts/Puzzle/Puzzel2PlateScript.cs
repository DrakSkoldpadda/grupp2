using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzel2PlateScript : MonoBehaviour
{
    public GameObject dörren;

    public float hurMktNer;

    public AudioSource stenLjud;
    public AudioSource openLjud;
    public AudioSource dundundundun;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(PlattaNer());
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

        yield return new WaitForSeconds(0.5f);

        OpenDoor();

        yield return new WaitForSeconds(2f);

        dundundundun.Play();

    }

    void OpenDoor()
    {
        dörren.GetComponent<Puzzel2Script>().OpenDaDoor();
        openLjud.Play();
    }

}
