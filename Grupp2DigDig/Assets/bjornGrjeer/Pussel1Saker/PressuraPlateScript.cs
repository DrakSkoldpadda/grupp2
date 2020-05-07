using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]  //måste ha en trigger och en vanlig

public class PressuraPlateScript : MonoBehaviour
{
    BoxCollider kollider;
    public float timeToGoDown;
    public float length;
    bool uppe;
    bool inAction;
    public GameObject doerrSomSkaOeppnas;
    [Header("Tag")]
    public string tagSomAktiverar;


    AudioSource ljud;

    [Header("Ljud")]
    public AudioSource ljudForStenarna;
    public AudioSource waterStenarnaLjud;
    bool harspelatStenLjud;

    // Start is called before the first frame update
    void Start()
    {
        uppe = true;
        kollider = GetComponent<BoxCollider>();

        ljud = GetComponent<AudioSource>();

        harspelatStenLjud = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagSomAktiverar)
            if (uppe == true && inAction == false)
                StartCoroutine(Ner());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagSomAktiverar)
            if (uppe == false && inAction == false)
                StartCoroutine(Upp());
    }

    IEnumerator Ner()
    {
        inAction = true;
        yield return new WaitForSeconds(0.28f);

        ljud.Play();
        print("Pressureplate ner");
        doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().OpenDoor();

        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0, -length / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;

        ljud.Stop();
        uppe = false;

        if (harspelatStenLjud == false)
        {
            waterStenarnaLjud.Play();

            yield return new WaitForSeconds(1.5f);
            ljudForStenarna.Play();
            harspelatStenLjud = true;
        }
    }

    IEnumerator Upp()
    {
        inAction = true;
        yield return new WaitForSeconds(0.68f);
        ljud.Play();

        print("Pressureplate upp");
        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0, length / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;
        ljud.Stop();

        uppe = true;
        doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().CloseDoor();
    }
}
