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

    // Start is called before the first frame update
    void Start()
    {
        uppe = true;
        kollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "nånting")
        if (uppe == true && inAction == false)
            StartCoroutine(Ner());
    }

    private void OnTriggerExit(Collider other)
    {
        if (uppe == false && inAction == false)
            StartCoroutine(Upp());
    }

    IEnumerator Ner()
    {
        inAction = true;
        yield return new WaitForSeconds(0.18f);

        print("Pressureplate ner");
        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0, -length / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;
        uppe = false;
        doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().OpenDoor();
    }

    IEnumerator Upp()
    {
        inAction = true;
        yield return new WaitForSeconds(0.18f);

        print("Pressureplate upp");
        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0,length / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;
        uppe = true;
        doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().CloseDoor();
    }
}
