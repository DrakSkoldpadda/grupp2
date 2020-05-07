using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBLpressuraPlate : MonoBehaviour
{
    BoxCollider kollider;
    public float timeToGoDown;
    public bool uppe;
    bool inAction;
    bool canmove;
    public GameObject doerrSomSkaOeppnas;

    public GameObject ConnectedPressurePlate;

    // Start is called before the first frame update
    void Start()
    {
        canmove = true;
        uppe = true;
        kollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)   // för player.
    {
        //if(other.gameObject.tag == "nånting")
        if (uppe == true && inAction == false && canmove)
            StartCoroutine(Ner());
    }

    private void OnCollisionEnter(Collision collision)   // collision enter är för saker man lägger på plattan.
    {
        if (collision.gameObject.tag == "moveableObject")
        {
            if (uppe == true && inAction == false)
                StartCoroutine(Ner());

            canmove = false;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "moveableObject")
        {
            if (uppe == false && inAction == false)
                StartCoroutine(Upp());

            canmove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (uppe == false && inAction == false && canmove)
            StartCoroutine(Upp());
    }

    IEnumerator Ner()
    {
        inAction = true;
        yield return new WaitForSeconds(0.48f);

        print("Pressureplate ner");
        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0, -0.30f / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;
        uppe = false;
        OpenCheckOpen();
    }

    IEnumerator Upp()
    {
        inAction = true;
        yield return new WaitForSeconds(0.48f);

        print("Pressureplate upp");
        for (int i = 0; i < timeToGoDown; i++)
        {
            transform.Translate(0, 0.30f / timeToGoDown, 0);
            yield return new WaitForFixedUpdate();
        }
        inAction = false;
        uppe = true;
        CloseCheckClose();
    }

    void OpenCheckOpen()
    {

        if (ConnectedPressurePlate.GetComponentInChildren<DBLpressuraPlate>().uppe == false)
            doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().OpenDoor();

    }

    void CloseCheckClose()
    {
            doerrSomSkaOeppnas.GetComponent<DoerrScriptt>().CloseDoor();
    }

}
